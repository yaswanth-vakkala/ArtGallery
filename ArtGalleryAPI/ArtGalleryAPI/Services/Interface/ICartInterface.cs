using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;

namespace ArtGalleryAPI.Services.Interface
{
    public interface ICartInterface
    {
        Task<IEnumerable<Cart>> GetAllCartsForUserAsync(string userId);
        Task<Cart> CreateCartAsync(Cart newCart);
        Task<bool> DeleteCartAsync(Guid cartId);
    }
}
