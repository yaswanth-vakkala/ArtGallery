using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Services.Implementation
{
    public class WishlistService : IWishlistInterface
    {
        private readonly ApplicationDbContext dbContext;

        public WishlistService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<WishList>> GetAllWishlistsForUserAsync(string userId)
        {
            var wishlists = await dbContext.WishList.Where(w => w.AppUserId == userId).ToListAsync();
            return wishlists;
        }

        public async Task<WishList> CreateWishlistAsync(WishList newWishlist)
        {
            var wishlist = await dbContext.WishList.FirstOrDefaultAsync(w => w.AppUserId == newWishlist.AppUserId && w.ProductId == newWishlist.ProductId);
            if (wishlist != null)
            {
                throw new Exception("Duplicate wishlist item!");
            }
            await dbContext.WishList.AddAsync(newWishlist);
            await dbContext.SaveChangesAsync();
            return newWishlist;
        }

        public async Task<bool> DeleteWishlistAsync(Guid wishlistId)
        {
            var wishlist = await dbContext.WishList.SingleOrDefaultAsync(w => w.WishListId == wishlistId);
            if (wishlist == null)
            {
                return false;
            }
            else
            {
                dbContext.WishList.Remove(wishlist);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteWishlistsAsync(Guid[] productIds)
        {
            foreach (var productId in productIds)
            {
                var wishlists = await dbContext.WishList.Where(w => w.ProductId == productId).ToListAsync();
                foreach (var wishlist in wishlists)
                {
                    dbContext.Remove(wishlist);
                }
            }
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
