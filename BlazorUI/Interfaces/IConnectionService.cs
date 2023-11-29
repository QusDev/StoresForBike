using System.Data.SqlClient;

namespace BlazorUI.Interfaces
{
    public interface IConnectionService
    {
        SqlConnection GetConnection();
        
    }
}
