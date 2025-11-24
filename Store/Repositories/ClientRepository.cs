

using Store.Domain;
using Store.Infrastructure;
using Store.Model;

namespace Store.Repositories
{
    internal class ClientRepository : IDao<Client>
    {
        private readonly SqlConnectionProvider _connectProvider;
        public ClientRepository(SqlConnectionProvider connectProvider)
        {
            this._connectProvider = connectProvider;
        }
        public Client Add(Client entity)
        {
            string qurey = "INSERT INTO Client(Name, Email,PhoneNumber,AddressID) OUTPUT INSERTED.ID " +
                "VALUES (@Name,@Email,@PhoneNumber,@AddressID)";

            using var connectProvider = this._connectProvider.CreateOpenConnection();
            using var cmd = connectProvider.CreateCommand();

            cmd.CommandText = qurey;
            cmd.AddParam("@Name", entity.Name);
            cmd.AddParam("@Email", entity.Email);
            cmd.AddParam("@PhoneNumber", entity.PhoneNumber);
            cmd.AddParam("@AddressID", entity.Address.Id);

            entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
            return entity;
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM Client WHERE ID = @ID ";

            using var connectProvider = this._connectProvider.CreateOpenConnection();
            using var cmd = connectProvider.CreateCommand();

            cmd.CommandText = query;
            cmd.AddParam("@ID", id);

            return cmd.ExecuteNonQuery() > 0;
        }

        public Client? GetById(int id)
        {
            string qurey = "SELECT ID , Name , Email ,PhoneNumber FROM Client WHERE ID = @ID";

            using var sql = this._connectProvider.CreateOpenConnection();
            using var cmd = sql.CreateCommand();

            cmd.CommandText = qurey;
            cmd.AddParam("@ID", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                int? clienteID = (int)Convert.ToInt64(reader["ID"]);
                string? name = reader["Name"].ToString();
                string? email = reader["Email"].ToString();
                string? phoneNumber = reader["PhoneNumber"].ToString();
                return new Client(clienteID ?? 0, name ?? "", email ?? "", phoneNumber ?? "", null);
            }

            return null;
        }

        public Client? Update(int id, Client entity)
        {
            string query = "UPDATE Client SET Name = @Name , Email = @Email , PhoneNumber = @PhoneNumber , AddressID = @AddressID WHERE ID = @ID";

            using var connectProvider = this._connectProvider.CreateOpenConnection();
            using var cmd = connectProvider.CreateCommand();

            cmd.CommandText = query;
            cmd.AddParam("@ID", id);
            cmd.AddParam("@Name", entity.Name);
            cmd.AddParam("@Email", entity.Email);
            cmd.AddParam("@PhoneNumber", entity.PhoneNumber);
            cmd.AddParam("@AddressID", entity.Address.Id);

            return cmd.ExecuteNonQuery() > 0 ? entity : null;

        }

    }
}
