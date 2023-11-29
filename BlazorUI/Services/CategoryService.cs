using BlazorUI.Interfaces;
using BlazorUI.Models;
using BlazorUI.Pages;
using Dapper;

namespace BlazorUI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IConnectionService _connectionService;

        public CategoryService(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task AddCategoryAsync(Category category)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = $"INSERT INTO Category (CategoryName) VALUES (@CategoryName)";
            await connection.QueryAsync<Category>(query, category);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = $"DELETE FROM Category WHERE CategoryId = {categoryId}";
            await connection.QueryAsync<Category>(query);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = "SELECT * FROM Categories";
            return await connection.QueryAsync<Category>(query);
        }

        public async Task<Category> GetCategoryAsync(int categoryId)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var query = $"SELECT * FROM Category WHERE CategoryId = {categoryId}";
            return await connection.QueryFirstAsync<Category>(query);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            using var connection = _connectionService.GetConnection();
            await connection.OpenAsync();

            var oldCategory = await GetCategoryAsync(category.CategoryId);

            oldCategory.CategoryName = category.CategoryName ?? oldCategory.CategoryName;

            var query = $"UPDATE Category SET CategoryName = @CategoryName WHERE CategoryId = @CategoryId";
            await connection.QueryAsync<Category>(query, category);
        }
    }
}
