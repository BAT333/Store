using Store.Domain;
using Store.Infrastructure;
using Store.Model;
using System.Diagnostics;

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
            using var transaction = connection.BeginTransaction();
            using var cmd = connection.CreateCommand();

            cmd.Transaction = transaction;
            cmd.CommandText = query;

            cmd.AddParam("@ClientID", entity.IdClient);
            cmd.AddParam("@ProductID", entity.IdProduct);

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
            string query = "DELETE FROM Cart WHERE ID = @ID";

            using var connection = this._connectionProvider.CreateOpenConnection();
            using var transaction = connection.BeginTransaction();
            using var cmd = connection.CreateCommand();

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

        public Cart? GetById(int id)
        {
            string query = "SELECT ID,  ClientID , ProductID FROM Cart WHERE ID = @ID";

            using var connection = this._connectionProvider.CreateOpenConnection();
            using var transaction = connection.BeginTransaction();
            using var cmd = connection.CreateCommand();

            cmd.Transaction = transaction;
            cmd.CommandText = query;

            cmd.AddParam("@ID", id);

            try
            {
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int cardID = Convert.ToInt32(reader["ID"]);
                    int clientID = Convert.ToInt32(reader["ClientID"]);
                    int productID = Convert.ToInt32(reader["ProductID"]);

                    transaction.Commit();
                    return new Cart(cardID, clientID, productID);
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

        public Cart? Update(int id, Cart entity)
        {
            string query = "UPDATE Cart SET ProductID = @ProductID WHERE ID = @ID";

            using var connection = this._connectionProvider.CreateOpenConnection();
            using var transaction = connection.BeginTransaction();
            using var cmd = connection.CreateCommand();

            cmd.Transaction = transaction;
            cmd.CommandText = query;

            cmd.AddParam("@ProductID", entity.IdProduct);
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
