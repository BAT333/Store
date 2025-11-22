using Store.Infrastructure;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositories
{
    internal class StoreRepository : IDao<Cart>
    {
        private readonly SqlConnectionProvider _connectionProvider;

        public StoreRepository(SqlConnectionProvider connectionProvider)
        {
            this._connectionProvider = connectionProvider;
        }

        public Cart Add(Cart entity)
        {
            string query = "INSERT INTO Cart ( ClientID , ProductID) OUTPUT INSERTED.ID VALUES (@ClientID, @ProductID)";

            using var connection = this._connectionProvider.CreateOpenConnection();
            using var cmd = connection.CreateCommand();

            cmd.CommandText = query;
            cmd.AddParam("@ClientID", entity.IdClient);
            cmd.AddParam("@ProductID", entity.IdProduct);

            entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
            return entity;

        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM Cart WHERE ID = @ID";

            using var connection = this._connectionProvider.CreateOpenConnection();
            using var cmd = connection.CreateCommand();

            cmd.CommandText = query;
            cmd.AddParam("@ID", id);

            return cmd.ExecuteNonQuery() > 0;
        }

        public Cart? GetById(int id)
        {
            string query = "SELECT ID,  ClientID , ProductID FROM Cart WHERE ID = @ID";

            using var connection = this._connectionProvider.CreateOpenConnection();
            using var cmd = connection.CreateCommand();

            cmd.CommandText = query;
            cmd.AddParam("@ID", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int cardID = Convert.ToInt32(reader["ID"]);
                int clientID = Convert.ToInt32(reader["ClientID"]);
                int productID = Convert.ToInt32(reader["ProductID"]);

                return new Cart(cardID, clientID, productID);
            }

            return null;
        }

        public Cart? Update(int id, Cart entity)
        {
            string query = "UPDATE Cart SET ProductID = @ProductID WHERE ID = @ID";

            using var connection = this._connectionProvider.CreateOpenConnection();
            using var cmd = connection.CreateCommand();

            cmd.CommandText = query;
            cmd.AddParam("@ProductID", entity.IdProduct);
            cmd.AddParam("@ID", id);

            return cmd.ExecuteNonQuery() > 0 ? entity : null;
        }
    }
}
