using Store.Infrastructure;
using Store.Domain;
using Store.Model;
using System.Diagnostics;
namespace Store.Repositories
{
    internal class ProductRepository : IDao<Product>
    {
        private readonly SqlConnectionProvider _connectionProvider;

        public ProductRepository(SqlConnectionProvider connectionProvider)
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
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex.GetBaseException();
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
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex.GetBaseException();
            }

        }

        public Product? GetById(int id)
        {
            string query = "SELECT ID, Name, Description ,Price FROM Product WHERE ID = @ID";

            using var connectionProvider = this._connectionProvider.CreateOpenConnection();
            using var transaction = connectionProvider.BeginTransaction();
            using var cmd = connectionProvider.CreateCommand();

            cmd.Transaction = transaction;
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

                    transaction.Commit();
                    return new Product(productID, name ?? "", description ?? "", price);

                }
                transaction.Rollback();
                return null;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex.GetBaseException();
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
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex.GetBaseException();
            }
        }
    }
}
