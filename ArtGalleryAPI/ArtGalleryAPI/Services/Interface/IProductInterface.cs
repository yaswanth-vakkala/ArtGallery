using ArtGalleryAPI.Models.Domain;

namespace ArtGalleryAPI.Services.Interface
{
    public interface IProductInterface
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
