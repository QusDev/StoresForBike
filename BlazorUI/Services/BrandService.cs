using BlazorUI.Interfaces;
using BlazorUI.Models;
using Dapper;

namespace BlazorUI.Services
{
    public class BrandService : IBrandService
    {
        private IConnectionService _connectionService;

        public BrandService(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task AddBrandAsync(Brand brand)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = $"INSERT INTO Brands (BrandName) VALUES (@BrandName)";
            await connection.QueryAsync<Brand>(query, brand);
        }

        public async Task DeleteBrandAsync(int brandId)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = $"DELETE FROM Brands WHERE BrandId = {brandId}";
            await connection.QueryAsync<Brand>(query);
        }

        public async Task<Brand> GetBrand(int brandId)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = $"SELECT * FROM Brands WHERE BrandId = {brandId}";
            return await connection.QueryFirstAsync<Brand>(query);
        }

        public async Task<IEnumerable<Brand>> GetBrandsAsync()
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = "SELECT * FROM Brands";
            return await connection.QueryAsync<Brand>(query);
        }

        public async Task UpdateBrandAsync(Brand brand)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var oldBrand = await GetBrand(brand.BrandId);

            oldBrand.BrandName = brand.BrandName ?? oldBrand.BrandName;

            var query = $"UPDATE Brands SET BrandName = @BrandName WHERE BrandId = @BrandId";
            await connection.QueryAsync<Brand>(query, brand);
        }
    }
}
