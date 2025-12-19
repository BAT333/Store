using Store.Domain;
using Store.Domain.Model.Dao;
using Store.Domain.Model.Infrastructure;
using Store.Infrastructure;
using Store.Infrastructure.ExceptionCustomized;
using System.Data;
using System.Data.Common;

namespace Store.Repositories
{
    internal class ClientRepository : IDaoClient<Client>
    {
        private readonly IConnectionSQL<IDbConnection> _connectProvider;
        public ClientRepository(IConnectionSQL<IDbConnection> connectProvider)
        {
            this._connectProvider = connectProvider;
        }
        public Client Add(Client entity)
        {

            Address address = this.AddAndress(entity.Address);

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
            cmd.AddParam("@AddressID", address.Id);

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

        private Address AddAndress(Address entity)
        {
            string query = "INSERT INTO addresses(City,State,Neighborhood,Number,ZipCode)" +
              "OUTPUT INSERTED.ID VALUES (@City, @State,@Neighborhood,@Number,@ZipCode)";

            using var connectionProvider = this._connectProvider.CreateOpenConnection();
            using var transaction = connectionProvider.BeginTransaction();
            using var cmd = connectionProvider.CreateCommand();

            cmd.Transaction = transaction;
            cmd.CommandText = query;

            cmd.AddParam("@City", entity.City);
            cmd.AddParam("@State", entity.State);
            cmd.AddParam("@Neighborhood", entity.Neighborhood);
            cmd.AddParam("@Number", entity.Number);
            cmd.AddParam("@ZipCode", entity.ZipCode);

            try
            {

                entity.Id = Convert.ToInt32(cmd.ExecuteScalar());

                transaction.Commit();

                return entity;

            }
            catch (DbException ex)
            {

                transaction.Rollback();
                throw new ExceptionalCustomer("Error registering address.", ex);

            }

        }

        public bool Delete(int id, int idAddress)
        {
            DeleteAndress(idAddress);

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

        private bool DeleteAndress(int id)
        {
            string query = "DELETE FROM addresses WHERE ID =  @ID";

            using var connectionProvider = this._connectProvider.CreateOpenConnection();
            using var transaction = connectionProvider.BeginTransaction();
            using var cmd = connectionProvider.CreateCommand();

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
                throw new ExceptionalCustomer("ERROR DELETING ADDRESSES.", ex);

            }
        }

        private Address GetAddressById(int id)
        {
            string query = "SELECT ID , City,State,Neighborhood,Number,ZipCode FROM addresses WHERE ID = @ID";

            using var connectionProvider = this._connectProvider.CreateOpenConnection();
            using var cmd = connectionProvider.CreateCommand();

            cmd.CommandText = query;

            cmd.AddParam("@ID", id);

            try
            {
                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    int? addressID = (int)Convert.ToInt64(reader["ID"]);
                    string? city = reader["City"].ToString();
                    string? state = reader["State"].ToString();
                    string? neighborhood = reader["Neighborhood"].ToString();
                    int? number = (int)Convert.ToInt64(reader["Number"]);
                    string? zipCode = reader["ZipCode"].ToString();

                    return new Address(addressID ?? 0, city ?? "", state ?? "", neighborhood ?? "", number ?? 0, zipCode ?? "");
                }

                return null;
            }
            catch (DbException ex)
            {

                throw new ExceptionalCustomer("ERROR SEARCHING FOR ADDRESSES.", ex);

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
                    Address address = GetAddressById((int)Convert.ToInt64(reader["AddressID"]))!;

                    return new Client(clienteID ?? 0, name ?? "", email ?? "", phoneNumber ?? "", address);
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
            UpdateAndress(id, entity.Address);
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

        private Address? UpdateAndress(int id, Address entity)
        {
            string query = "UPDATE addresses SET City = @City ,State = @State ,Neighborhood = @Neighborhood ,Number = @Number ,ZipCode = @ZipCode WHERE ID = @ID ";

            using var connectionProvider = this._connectProvider.CreateOpenConnection();
            using var transaction = connectionProvider.BeginTransaction();
            using var cmd = connectionProvider.CreateCommand();

            cmd.Transaction = transaction;
            cmd.CommandText = query;

            cmd.AddParam("@ID", id);
            cmd.AddParam("@City", entity.City);
            cmd.AddParam("@State", entity.State);
            cmd.AddParam("@Neighborhood", entity.Neighborhood);
            cmd.AddParam("@Number", entity.Number);
            cmd.AddParam("@ZipCode", entity.ZipCode);

            try
            {
                bool update = cmd.ExecuteNonQuery() > 0;
                if (update)
                {
                    transaction.Commit();
                    return entity;
                }
                transaction.Rollback();
                return null;
            }
            catch (DbException ex)
            {
                transaction.Rollback();
                throw new ExceptionalCustomer("Error updating addresses.", ex);
            }
        }
    }
}
