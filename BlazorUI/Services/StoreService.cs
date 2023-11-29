using BlazorUI.Interfaces;
using BlazorUI.Models;
using Dapper;

namespace BlazorUI.Services
{
    public class StoreService : IStoreService
    {
        private readonly IConnectionService _connectionService;

        public StoreService(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task AddStoreAsync(Store store)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = $"INSERT INTO Stores (" +
                            $"StoreName," +
                            $"Phone," +
                            $"Email," +
                            $"City," +
                            $"State," +
                            $"Street," +
                            $"ZipCode)" +
                         $"VALUES (" +
                            $"@StoreName," +
                            $"@Phone," +
                            $"@Email," +
                            $"@City," +
                            $"@State," +
                            $"@Street," +
                            $"@ZipCode)";

            await connection.QueryAsync<Store>(query, store);
        }

        public async Task DeleteStoreAsync(int storeId)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = $"DELETE FROM Stores WHERE StoreId = {storeId}";
            await connection.QueryAsync<Store>(query);
        }

        public async Task<Store> GetStoreAsync(int storeId)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = $"SELECT * FROM Stores WHERE StoreId = {storeId}";
            return await connection.QueryFirstAsync<Store>(query);
        }

        public async Task<IEnumerable<Store>> GetStoresAsync()
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = "SELECT * FROM Stores";
            return await connection.QueryAsync<Store>(query);
        }

        public async Task UpdateStoreAsync(Store store)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var oldStore = await GetStoreAsync(store.StoreId);

            oldStore.StoreName = store.StoreName ?? oldStore.StoreName;
            oldStore.Phone = store.Phone ?? oldStore.Phone;
            oldStore.Email = store.Email ?? oldStore.Email;
            oldStore.City = store.City ?? oldStore.City;
            oldStore.State = store.State ?? oldStore.State;
            oldStore.Street = store.Street ?? oldStore.Street;
            oldStore.ZipCode = store.ZipCode ?? oldStore.ZipCode;


            var query = $"UPDATE Stores SET " +
                            $"StoreName = @StoreName," +
                            $"Phone = @Phone," +
                            $"Email = @Email," +
                            $"City = @City," +
                            $"State = @State," +
                            $"Street = @Street," +
                            $"ZipCode = @ZipCode " +
                         $"WHERE StoreId = @StoreId";

            await connection.QueryAsync<Store>(query, store);
        }
    }
}
