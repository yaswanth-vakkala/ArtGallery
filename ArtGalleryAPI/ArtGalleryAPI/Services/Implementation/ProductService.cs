using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Services.Implementation
{
    public class ProductService :  IProductInterface
    {
        private readonly ApplicationDbContext dbContext;
        public ProductService(ApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await dbContext.Product.ToListAsync();
            return products;
        }
    }
}
