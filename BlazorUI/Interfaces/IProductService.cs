using BlazorUI.Models;

namespace BlazorUI.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(int productId);
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(int productId);
        Task UpdateProductAsync(Product product);
    }
}
