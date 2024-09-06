using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;

namespace ArtGalleryAPI.Services.Interface
{
    public interface IWishlistInterface
    {
        Task<IEnumerable<WishList>> GetAllWishlistsForUserAsync(string userId);
        Task<WishList> CreateWishlistAsync(WishList newWishlist);
        Task<bool> DeleteWishlistAsync(Guid wishlistId);
        Task<bool> DeleteWishlistsAsync(Guid[] productIds);
    }
}
