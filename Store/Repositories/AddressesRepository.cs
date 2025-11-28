using Store.Infrastructure;
using Store.Domain;
using Store.Model;


namespace Store.Repositories
{
    internal class AddressesRepository : IDao<Address>
    {
        private readonly SqlConnectionProvider _connectionProvider;
        public AddressesRepository(SqlConnectionProvider connectionProvider)
        {
            this._connectionProvider = connectionProvider;
        }
        public Address Add(Address entity)
        {
            string query = "INSERT INTO addresses(City,State,Neighborhood,Number,ZipCode)" +
                "OUTPUT INSERTED.ID VALUES (@City, @State,@Neighborhood,@Number,@ZipCode)";

            using var connectionProvider = this._connectionProvider.CreateOpenConnection();
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
            catch (Exception ex)
            {

                transaction.Rollback();
                throw ex.GetBaseException();

            }

        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM addresses WHERE ID =  @ID";

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

        public Address? GetById(int id)
        {
            string query = "SELECT ID , City,State,Neighborhood,Number,ZipCode FROM addresses WHERE ID = @ID";

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

                    int? addressID = (int)Convert.ToInt64(reader["ID"]);
                    string? city = reader["City"].ToString();
                    string? state = reader["State"].ToString();
                    string? neighborhood = reader["Neighborhood"].ToString();
                    int? number = (int)Convert.ToInt64(reader["Number"]);
                    string? zipCode = reader["ZipCode"].ToString();

                    transaction.Commit();

                    return new Address(addressID ?? 0, city ?? "", state ?? "", neighborhood ?? "", number ?? 0, zipCode ?? "");
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

        public Address? Update(int id, Address entity)
        {
            string query = "UPDATE addresses SET City = @City ,State = @State ,Neighborhood = @Neighborhood ,Number = @Number ,ZipCode = @ZipCode WHERE ID = @ID ";

            using var connectionProvider = this._connectionProvider.CreateOpenConnection();
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
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex.GetBaseException();
            }
        }
    }
}
