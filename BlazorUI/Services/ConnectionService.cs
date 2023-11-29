using BlazorUI.Interfaces;
using System.Data.SqlClient;

namespace BlazorUI.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly string _connectionString;

        public ConnectionService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public SqlConnection GetConnection() => new (_connectionString);
    }
}
