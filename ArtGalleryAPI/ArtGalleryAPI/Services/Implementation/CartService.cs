using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Services.Implementation
{
    public class CartService : ICartInterface
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CartService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
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
            var isAdmin = httpContextAccessor.HttpContext.User.IsInRole("Writer");
            var userId = httpContextAccessor.HttpContext?.User.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid").FirstOrDefault().Value;
            if (cart == null || (!isAdmin && cart.AppUserId != userId))
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
            var isAdmin = httpContextAccessor.HttpContext.User.IsInRole("Writer");
            var userId = httpContextAccessor.HttpContext?.User.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid").FirstOrDefault().Value;
            foreach (var productId in productIds) {
                var carts = await dbContext.Cart.Where(c => c.ProductId == productId).ToListAsync();
                foreach (var cart in carts) { 
                    if(!isAdmin && cart.AppUserId != userId)
                    {
                        return false;
                    }
                    dbContext.Remove(cart);
                }
            }
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
