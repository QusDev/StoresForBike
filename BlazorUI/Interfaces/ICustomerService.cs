using BlazorUI.Models;

namespace BlazorUI.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerAsync(int customerId);
        Task AddCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int customerId);
        Task UpdateCustomerAsync(Customer customer);
    }
}
