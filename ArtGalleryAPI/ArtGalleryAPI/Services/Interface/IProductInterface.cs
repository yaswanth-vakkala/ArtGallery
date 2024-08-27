using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;

namespace ArtGalleryAPI.Services.Interface
{
    public interface IProductInterface
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product>? GetProductByIdAsync(Guid productId);
        Task<IEnumerable<Product>>? GetProductsByCategoryIdAsync(Guid categoryId);
        Task<Product> CreateProductAsync(Product newProduct);
        Task<Product>? UpdateProductAsync(Guid productId, Product updatedProduct);
        Task<bool> DeleteProductAsync(Guid productId);
    }
}