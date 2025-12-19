using Microsoft.Data.SqlClient;
using Store.Domain.Model.Infrastructure;
using System.Data;
namespace Store.Infrastructure
{
    internal class SqlConnectionProvider : IConnectionSQL<IDbConnection>
    {


        private readonly string _connectionString ;

        public SqlConnectionProvider(string connectionString)
        {
            this. _connectionString = connectionString;
        }

        public IDbConnection CreateOpenConnection()
        {
            SqlConnection connection = new(_connectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (SqlException ex)
            {
                connection.Dispose();
                throw new ApplicationException("Falha ao abrir a conexão com o banco de dados.", ex);
            }

        }
    }
}
