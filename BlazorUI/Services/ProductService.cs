using BlazorUI.Interfaces;
using BlazorUI.Models;
using Dapper;

namespace BlazorUI.Services
{
    public class ProductService : IProductService
    {
        private readonly IConnectionService _connectionService;

        public ProductService(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task AddProductAsync(Product product)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = $"INSERT INTO Products (" +
                            $"ProductName," +
                            $"BrandId," +
                            $"CategoryId," +
                            $"Price," +
                            $"ModelYear) " +
                         $"VALUES (" +
                            $"@ProductName," +
                            $"@BrandId," +
                            $"@CategoryId," +
                            $"@Price," +
                            $"@ModelYear)";

            await connection.QueryAsync<Customer>(query, product);
        }

        public async Task DeleteProductAsync(int productId)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = $"DELETE FROM Products WHERE ProductId = {productId}";
            await connection.QueryAsync<Product>(query);
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = $"SELECT * FROM Products WHERE ProductId = {productId}";
            return await connection.QueryFirstAsync<Product>(query);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = "SELECT * FROM Products";
            return await connection.QueryAsync<Product>(query);
        }

        public async Task UpdateProductAsync(Product product)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var oldProduct = await GetProductAsync(product.ProductId);

            oldProduct.ProductName = product.ProductName ?? oldProduct.ProductName;
            oldProduct.Price = product.Price;
            oldProduct.BrandId = product.BrandId;
            oldProduct.CategoryId = product.CategoryId;
            oldProduct.Price = product.Price;
            oldProduct.ModelYear = product.ModelYear;

            var query = $"UPDATE Products SET " +
                            $"ProductName = @ProductName," +
                            $"Price = @Price," +
                            $"BrandId = @BrandId," +
                            $"CategoryId = @CategoryId," +
                            $"ModelYear = @ModelYear " +
                         $"WHERE ProductId = @ProductId";

            await connection.QueryAsync<Product>(query, product);
        }
    }
}
