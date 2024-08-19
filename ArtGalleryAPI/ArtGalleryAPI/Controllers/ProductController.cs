using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductInterface productService;
        public ProductController(IProductInterface productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productService.GetAllProductsAsync();
            return Ok(products);
        }

        /*[HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDto product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Status = "Active",
                CreatedAt = DateTime.UtcNow,
            };
            await dbContext.Product.AddAsync(newProduct);
            await dbContext.SaveChangesAsync();
            return Ok(newProduct);
        }*/
    }
}
