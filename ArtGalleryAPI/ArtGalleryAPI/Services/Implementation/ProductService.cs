using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
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

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            var product = await dbContext.Product.SingleOrDefaultAsync(product => product.ProductId == productId);
            return product;
        }

        public async Task<Product> CreateProductAsync(Product newProduct)
        {
            await dbContext.Product.AddAsync(newProduct);
            await dbContext.SaveChangesAsync();
            return newProduct;
        }
    }
}
