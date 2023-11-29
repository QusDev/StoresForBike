using BlazorUI.Models;

namespace BlazorUI.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetBrandsAsync();
        Task<Brand> GetBrand(int brandId);
        Task AddBrandAsync(Brand brand);
        Task DeleteBrandAsync(int brandId);
        Task UpdateBrandAsync(Brand brand);
    }
}
