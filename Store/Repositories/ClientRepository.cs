

using Store.Domain;
using Store.Infrastructure;
using Store.Infrastructure.ExceptionCustomized;
using System.Data.Common;
using Store.Domain.Model.Dao;

namespace Store.Repositories
{
    internal class ClientRepository : IDaoClient<Client>
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
            using var transaction = connectProvider.BeginTransaction();
            using var cmd = connectProvider.CreateCommand();

            cmd.Transaction = transaction;
            cmd.CommandText = qurey;

            cmd.AddParam("@Name", entity.Name);
            cmd.AddParam("@Email", entity.Email);
            cmd.AddParam("@PhoneNumber", entity.PhoneNumber);
            cmd.AddParam("@AddressID", entity.Address.Id);

            try
            {
                entity.Id = Convert.ToInt32(cmd.ExecuteScalar());

                transaction.Commit();

                return entity;
            }
            catch (DbException ex)
            {
                transaction.Rollback();
                throw new ExceptionalCustomer("Error registering customer.", ex);
            }
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM Client WHERE ID = @ID ";

            using var connectProvider = this._connectProvider.CreateOpenConnection();
            using var transaction = connectProvider.BeginTransaction();
            using var cmd = connectProvider.CreateCommand();

            cmd.Transaction = transaction;
            cmd.CommandText = query;
            cmd.AddParam("@ID", id);

            try
            {
                bool delete = cmd.ExecuteNonQuery() > 0;
                if (!delete)
                {
                    transaction.Rollback();
                    return delete;
                }


                transaction.Commit();
                return delete;
            }
            catch (DbException ex)
            {
                transaction.Rollback();
                throw new ExceptionalCustomer("ERROR DELETING CLIENT.", ex);
            }
        }

        public Client? GetById(int id)
        {
            string qurey = "SELECT ID , Name , Email ,PhoneNumber,AddressID FROM Client WHERE ID = @ID";

            using var connectProvider = this._connectProvider.CreateOpenConnection();
            using var cmd = connectProvider.CreateCommand();

            cmd.CommandText = qurey;
            cmd.AddParam("@ID", id);

            try
            {
                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int? clienteID = (int)Convert.ToInt64(reader["ID"]);
                    string? name = reader["Name"].ToString();
                    string? email = reader["Email"].ToString();
                    string? phoneNumber = reader["PhoneNumber"].ToString();
                    int? addressID = (int)Convert.ToInt64(reader["AddressID"]);

                    return new Client(clienteID ?? 0, name ?? "", email ?? "", phoneNumber ?? "", new Address(addressID ?? 0));
                }

                return null;
            }
            catch (DbException ex)
            {
                throw new ExceptionalCustomer("ERROR SEARCHING FOR CLIENT", ex);
            }

        }

        public Client? Update(int id, Client entity)
        {
            string query = "UPDATE Client SET Name = @Name , Email = @Email , PhoneNumber = @PhoneNumber  WHERE ID = @ID";

            using var connectProvider = this._connectProvider.CreateOpenConnection();
            using var transaction = connectProvider.BeginTransaction();
            using var cmd = connectProvider.CreateCommand();

            cmd.Transaction = transaction;
            cmd.CommandText = query;

            cmd.AddParam("@ID", id);
            cmd.AddParam("@Name", entity.Name);
            cmd.AddParam("@Email", entity.Email);
            cmd.AddParam("@PhoneNumber", entity.PhoneNumber);

            try
            {
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    transaction.Commit();
                    return entity;
                }
                else
                {
                    transaction.Rollback();
                    return null;
                }
            }
            catch (DbException ex)
            {
                transaction.Rollback();
                throw new ExceptionalCustomer("Error updating client.", ex);
            }

        }

    }
}
