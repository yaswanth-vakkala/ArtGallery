using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ArtGalleryAPI.Services.Interface
{
    public interface IProductInterface
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(string? sortBy=null, string? sortOrder=null);
        Task<Product>? GetProductByIdAsync(Guid productId);
        Task<IEnumerable<Product>> GetProductsFromIdArrayAsync(IEnumerable<Guid> productIds);
        Task<IEnumerable<Product>>? GetProductsByCategoryIdAsync(Guid categoryId);
        Task<Product> CreateProductAsync(Product newProduct);
        Task<Product>? UpdateProductAsync(Guid productId, UpdateProductDto updatedProduct);
        Task<bool> DeleteProductAsync(Guid productId);
        Task<bool> DeleteProductsAsync(Guid[] productIds);
    }
}