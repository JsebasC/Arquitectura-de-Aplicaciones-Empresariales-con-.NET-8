using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pacagroup.Ecommerce.Transversal.Common;
using System.Data;

namespace Pacagroup.Ecommerce.Infrastructure.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection? GetConnection
        {
            get
            {
                SqlConnection? sqlConnection = new();
                if (sqlConnection is null) return null;

                sqlConnection.ConnectionString = _configuration.GetConnectionString("NorthwindConnection");
                sqlConnection.Open();
                return sqlConnection;
            }
        }
    }
}
