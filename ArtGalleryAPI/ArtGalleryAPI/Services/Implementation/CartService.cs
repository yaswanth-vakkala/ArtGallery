using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Services.Implementation
{
    public class CartService : ICartInterface
    {
        private readonly ApplicationDbContext dbContext;

        public CartService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<Cart>> GetAllCartsForUserAsync(string UserId)
        {
            var carts = await dbContext.Cart.Where(c => c.AppUserId == UserId).ToListAsync();
            return carts;
        }
        public async Task<Cart> CreateCartAsync(Cart newCart)
        {
            var cart = await dbContext.Cart.FirstOrDefaultAsync(c => c.AppUserId == newCart.AppUserId && c.ProductId == newCart.ProductId);
            if (cart != null)
            {
                throw new Exception("duplicate cart item!");
            }
            await dbContext.Cart.AddAsync(newCart);
            await dbContext.SaveChangesAsync();
            return newCart;
        }

        public async Task<bool> DeleteCartAsync(Guid cartId)
        {
            var cart = await dbContext.Cart.SingleOrDefaultAsync(c => c.CartId == cartId);
            if (cart == null)
            {
                return false;
            }
            else
            {
                dbContext.Cart.Remove(cart);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteCartsAsync(Guid[] productIds)
        {
            foreach (var productId in productIds) {
                var carts = await dbContext.Cart.Where(c => c.ProductId == productId).ToListAsync();
                foreach (var cart in carts) { 
                    dbContext.Remove(cart);
                }
            }
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
