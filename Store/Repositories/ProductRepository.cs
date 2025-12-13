using Store.Domain;
using Store.Domain.Model.Dao;
using Store.Domain.Model.Infrastructure;
using Store.Infrastructure;
using Store.Infrastructure.ExceptionCustomized;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
namespace Store.Repositories
{
    internal class ProductRepository : IDaoProduct<Product>
    {
        private readonly IConnectionSQL<IDbConnection> _connectionProvider;

        public ProductRepository(IConnectionSQL<IDbConnection> connectionProvider)
        {
            this._connectionProvider = connectionProvider;
        }
        public Product Add(Product entity)
        {
            string query = "INSERT INTO Product(Name,Description,Price) OUTPUT INSERTED.ID " +
    "VALUES (@Name,@Description,@Price)";

            using var connectionProvider = this._connectionProvider.CreateOpenConnection();
            using var transaction = connectionProvider.BeginTransaction();
            using var cmd = connectionProvider.CreateCommand();

            cmd.Transaction = transaction;
            cmd.CommandText = query;

            cmd.AddParam("@Name", entity.Name);
            cmd.AddParam("@Description", entity.Description);
            cmd.AddParam("@Price", entity.Price);

            try
            {
                entity.Id = Convert.ToInt32(cmd.ExecuteScalar());

                transaction.Commit();

                return entity;

            }
            catch (DbException ex)
            {
                transaction.Rollback();
                throw new ExceptionalProduct("Error registering product.", ex);
            }
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM Product WHERE Id = @ID";

            using var connectionProvider = this._connectionProvider.CreateOpenConnection();
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
                throw new ExceptionalProduct("ERROR DELETING PRODUCT.", ex);
            }

        }

        public Product? GetById(int id)
        {
            string query = "SELECT ID, Name, Description ,Price FROM Product WHERE ID = @ID";

            using var connectionProvider = this._connectionProvider.CreateOpenConnection();
            using var cmd = connectionProvider.CreateCommand();

            cmd.CommandText = query;

            cmd.AddParam("@ID", id);

            try
            {
                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int productID = Convert.ToInt32(reader["ID"]);
                    string? name = reader["Name"].ToString();
                    string? description = reader["Description"].ToString();
                    double price = Convert.ToDouble(reader["Price"]);

                    return new Product(productID, name ?? "", description ?? "", price);

                }
                return null;
            }
            catch (DbException ex)
            {
                throw new ExceptionalProduct("ERROR SEARCHING FOR PRODUCT", ex);
            }


        }

        public Product? Update(int id, Product entity)
        {
            string query = "UPDATE Product SET Name = @Name, Description = @Description ,Price = @Price WHERE ID = @ID";

            using var connectionProvider = this._connectionProvider.CreateOpenConnection();
            using var transaction = connectionProvider.BeginTransaction();
            using var cmd = connectionProvider.CreateCommand();

            cmd.Transaction = transaction;
            cmd.CommandText = query;

            cmd.AddParam("@Name", entity.Name);
            cmd.AddParam("@Description", entity.Description);
            cmd.AddParam("@Price", entity.Price);
            cmd.AddParam("@ID", id);


            try
            {
                bool update = cmd.ExecuteNonQuery() > 0;

                if (!update)
                {
                    transaction.Rollback();
                    return null;
                }
                transaction.Commit();
                return entity;

            }
            catch (DbException ex)
            {
                transaction.Rollback();
                throw new ExceptionalProduct("Error updating Product.", ex);
            }
        }
    }
}
