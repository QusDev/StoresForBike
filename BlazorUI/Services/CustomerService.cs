using BlazorUI.Interfaces;
using BlazorUI.Models;
using Dapper;

namespace BlazorUI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IConnectionService _connectionService;

        public CustomerService(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = $"INSERT INTO Customers (" +
                            $"FirstName," +
                            $"LastName," +
                            $"Phone," +
                            $"Email," +
                            $"City," +
                            $"State," +
                            $"Street," +
                            $"ZipCode)" +
                         $"VALUES (" +
                            $"@FirstName," +
                            $"@LastName," +
                            $"@Phone," +
                            $"@Email," +
                            $"@City," +
                            $"@State," +
                            $"@Street," +
                            $"@ZipCode)";

            await connection.QueryAsync<Customer>(query, customer);
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = $"DELETE FROM Customers WHERE CustomerId = {customerId}";
            await connection.QueryAsync<Customer>(query);
        }

        public async Task<Customer> GetCustomerAsync(int customerId)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = $"SELECT * FROM Customers WHERE CustomerId = {customerId}";
            return await connection.QueryFirstAsync<Customer>(query);
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = "SELECT * FROM Customers";
            return await connection.QueryAsync<Customer>(query);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var oldCustomer = await GetCustomerAsync(customer.CustomerId);

            oldCustomer.FirstName = customer.FirstName ?? oldCustomer.FirstName;
            oldCustomer.LastName = customer.LastName ?? oldCustomer.LastName;
            oldCustomer.Phone = customer.Phone ?? oldCustomer.Phone;
            oldCustomer.Email = customer.Email ?? oldCustomer.Email;
            oldCustomer.City = customer.City ?? oldCustomer.City;
            oldCustomer.State = customer.State ?? oldCustomer.State;
            oldCustomer.Street = customer.Street ?? oldCustomer.Street;
            oldCustomer.ZipCode = customer.ZipCode ?? oldCustomer.ZipCode;


            var query = $"UPDATE Customers SET " +
                            $"FirstName = @FirstName," +
                            $"LastName = @LastName," +
                            $"Phone = @Phone," +
                            $"Email = @Email," +
                            $"City = @City," +
                            $"State = @State," +
                            $"Street = @Street," +
                            $"ZipCode = @ZipCode " +
                         $"WHERE CustomerId = @CustomerId ";

            await connection.QueryAsync<Customer>(query, customer);
        }
    }
}
