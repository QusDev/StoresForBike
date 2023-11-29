using BlazorUI.Models;

namespace BlazorUI.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(int categoryId);
        Task AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(int categoryId);
        Task UpdateCategoryAsync(Category category);
    }
}
