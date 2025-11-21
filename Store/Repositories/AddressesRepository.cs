using Store.Infrastructure;
using Store.Models;


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
            using var cmd = connectionProvider.CreateCommand();

            cmd.CommandText = query;
            cmd.AddParam("@City", entity.City);
            cmd.AddParam("@State", entity.State);
            cmd.AddParam("@Neighborhood", entity.Neighborhood);
            cmd.AddParam("@Number", entity.Number);
            cmd.AddParam("@ZipCode", entity.ZipCode);

            entity.Id = (int)cmd.ExecuteScalar();
            return entity;

        }

        public bool Delete(int id)
        {
            string query = "DELETE addresses WHERE ID =  @ID";

            using var connectionProvider = this._connectionProvider.CreateOpenConnection();
            using var cmd = connectionProvider.CreateCommand();

            cmd.CommandText = query;
            cmd.AddParam("@ID", id);

            return cmd.ExecuteNonQuery() > 0;
        }

        public Address? GetById(int id)
        {
            string query = "SELECT ID , City,State,Neighborhood,Number,ZipCode FROM addresses WHERE ID = @ID";

            using var connectionProvider = this._connectionProvider.CreateOpenConnection();
            using var cmd = connectionProvider.CreateCommand();

            cmd.CommandText = query;
            cmd.AddParam("@ID", id);

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

        public Address? Update(int id, Address entity)
        {
            string query = "UPDATE addresses SET City = @City ,State = @State ,Neighborhood = @Neighborhood ,Number = @Number ,ZipCode = @ZipCode WHERE ID = @ID ";

            using var connectionProvider = this._connectionProvider.CreateOpenConnection();
            using var cmd = connectionProvider.CreateCommand();

            cmd.CommandText = query;
            cmd.AddParam("@ID", id);
            cmd.AddParam("@City", entity.City);
            cmd.AddParam("@State", entity.State);
            cmd.AddParam("@Neighborhood", entity.Neighborhood);
            cmd.AddParam("@Number", entity.Number);
            cmd.AddParam("@ZipCode", entity.ZipCode);

            return cmd.ExecuteNonQuery() > 0? entity:null;
        }
    }
}
