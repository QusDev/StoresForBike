using BlazorUI.Models;

namespace BlazorUI.Interfaces
{
    public interface IStoreService
    {
        Task<IEnumerable<Store>> GetStoresAsync();
        Task<Store> GetStoreAsync(int storeId);
        Task AddStoreAsync(Store store);
        Task DeleteStoreAsync(int storeId);
        Task UpdateStoreAsync(Store store);
    }
}
