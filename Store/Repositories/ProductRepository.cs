using Store.Infrastructure;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            using var cmd = connectionProvider.CreateCommand();

            cmd.CommandText = query;
            cmd.AddParam("@Name", entity.Name);
            cmd.AddParam("@Description", entity.Description);
            cmd.AddParam("@Price", entity.Price);

            entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
            return entity;
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM Product WHERE Id = @ID";

            using var connectionProvider = this._connectionProvider.CreateOpenConnection();
            using var cmd = connectionProvider.CreateCommand();

            cmd.CommandText = query;
            cmd.AddParam("@ID", id);

            return cmd.ExecuteNonQuery() > 0;
        }

        public Product? GetById(int id)
        {
            string query = "SELECT ID, Name, Description ,Price FROM Product WHERE ID = @ID";

            using var connectionProvider = this._connectionProvider.CreateOpenConnection();
            using var cmd = connectionProvider.CreateCommand();

            cmd.CommandText = query;
            cmd.AddParam("@ID", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                int productID = Convert.ToInt32(reader["ID"]);
                string name = reader["Name"].ToString();
                string description = reader["Description"].ToString();
                double price = Convert.ToDouble(reader["Price"]);

                return new Product(productID, name, description, price);

            }
            return null;
        }

        public Product? Update(int id, Product entity)
        {
            string query = "UPDATE Product SET Name = @Name, Description = @Description ,Price = @Price WHERE ID = @ID";

            using var connectionProvider = this._connectionProvider.CreateOpenConnection();
            using var cmd = connectionProvider.CreateCommand();

            cmd.CommandText = query;

            cmd.AddParam("@Name", entity.Name);
            cmd.AddParam("@Description", entity.Description);
            cmd.AddParam("@Price", entity.Price);
            cmd.AddParam("@ID", id);

            return cmd.ExecuteNonQuery() > 0 ? entity : null;
        }
    }
}
