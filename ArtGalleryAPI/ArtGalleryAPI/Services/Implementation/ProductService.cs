using ArtGalleryAPI.CustomExceptions;
using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Services.Implementation
{
    public class ProductService : IProductInterface
    {
        private readonly ApplicationDbContext dbContext;
        public ProductService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(int pageNumber, int pageSize, string? query = null, string? sortBy = null, string? sortOrder = null, Guid? categoryId = null)
        {
            var skipResults = (pageNumber - 1) * pageSize;
            IEnumerable<Product> products = Enumerable.Empty<Product>();
            if (categoryId != null)
            {
                products = dbContext.Product.Include(c => c.Category).Where(p => p.Status == "In Stock" && p.Category.CategoryId == categoryId).AsQueryable();
            }
            else
            {
                products = dbContext.Product.Include(c => c.Category).Where(p => p.Status == "In Stock").AsQueryable();
            }

            if (string.IsNullOrWhiteSpace(query) == false)
            {
                products = products.Where(x => x.Name.Contains(query));
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (string.Equals(sortBy,"Price", StringComparison.OrdinalIgnoreCase))
                {
                    var isDesc = string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    products = isDesc ? products.OrderByDescending(p => p.Price) : products.OrderBy(p => p.Price);
                } else if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    var isDesc = string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    products = isDesc ? products.OrderByDescending(p => p.Name) : products.OrderBy(p => p.Name);
                }
            }

            products = products.Skip(skipResults).Take(pageSize);
            products = products.ToList();
            return products;
        }

        public async Task<Product>? GetProductByIdAsync(Guid productId)
        {
            var product = await dbContext.Product.Include(c => c.Category).SingleOrDefaultAsync(product => product.ProductId == productId);
            return product;
        }

        public async Task<int> GetProductsCountAsync(string? query, Guid? categoryId)
        {
            var productCount = 0;
            if (categoryId == null)
            {
                if (!string.IsNullOrWhiteSpace(query))
                {
                    productCount = await dbContext.Product.Where(p => p.Status == "In Stock" && p.Name.Contains(query)).CountAsync();
                }
                else
                {
                    productCount = await dbContext.Product.Where(p => p.Status == "In Stock").CountAsync();
                }
            }
            else
            {
                var products = dbContext.Product.Where(p => p.Category.CategoryId == categoryId);
                if (!string.IsNullOrWhiteSpace(query))
                {
                    productCount = await products.Where(p => p.Name.Contains(query) && p.Status == "In Stock").CountAsync();
                }
                else
                {
                    productCount = await products.Where(p => p.Status == "In Stock").CountAsync();
                }
            }
            return productCount;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            var products = await dbContext.Product.Include(c => c.Category).Where(p => p.Category.CategoryId == categoryId && p.Status == "In Stock").ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsFromIdArrayAsync(IEnumerable<Guid> productIds)
        {
            List<Product> products = new List<Product>();
            foreach(var id in productIds)
            {
                var product = await dbContext.Product.SingleOrDefaultAsync(p => p.ProductId == id);
                products.Add(product);
            }
            return products;
        }

        public async Task<Product> CreateProductAsync(Product newProduct)
        {
            await dbContext.Product.AddAsync(newProduct);
            await dbContext.SaveChangesAsync();
            return newProduct;
        }

        public async Task<Product>? UpdateProductAsync(Guid productId, UpdateProductDto updatedProduct)
        {
            var product = await dbContext.Product.Include(c => c.Category).SingleOrDefaultAsync(product => product.ProductId == productId);
            if (product == null)
            {
                return null;
            }
            else
            {
                var category = await dbContext.Category.SingleOrDefaultAsync(category => category.CategoryId == updatedProduct.CategoryId);
                if (category == null)
                {
                    return null;
                }
                else
                {
                    dbContext.Entry(product).CurrentValues.SetValues(updatedProduct);
                    product.Category = category;
                    await dbContext.SaveChangesAsync();
                    return product;
                }
            }
        }

        public async Task<bool> DeleteProductAsync(Guid productId)
        {
            var product = await dbContext.Product.SingleOrDefaultAsync(product => product.ProductId == productId);
            if (product == null)
            {
                return false;
            }
            else if (product.Status == "Deleted")
            {
                throw new InvalidDeletionException();
            }
            else
            {
                product.Status = "Deleted";
                await dbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteProductsAsync(Guid[] productIds)
        {
            foreach (var productId in productIds)
            {
                var products = await dbContext.Product.Where(p => p.ProductId == productId).ToListAsync();
                foreach (var product in products)
                {
                    product.Status = "Deleted";
                }
            }
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}