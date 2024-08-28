using ArtGalleryAPI.CustomExceptions;
using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductInterface productService;
        private readonly ICategoryInterface categoryService;

        public ProductController(IProductInterface productService, ICategoryInterface categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }
        /// <summary>
        /// returns all the active products from the database
        /// </summary>
        /// <returns>list of all products</returns>

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await productService.GetAllProductsAsync();
                List<ProductDto> result = new List<ProductDto>();
                foreach (Product product in products)
                {
                    result.Add(
                        new ProductDto()
                        {
                            ProductId = product.ProductId,
                            Name = product.Name,
                            Description = product.Description,
                            ImageUrl = product.ImageUrl,
                            Price = product.Price,
                            Status = product.Status,
                            CreatedAt = product.CreatedAt,
                            Category = product.Category
                        }
                        );
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// returns the filtered product record based on id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>filtered product</returns>
        [HttpGet]
        [Route("{productId:Guid}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid productId)
        {
            try
            {
                var product = await productService.GetProductByIdAsync(productId);
                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    var response = new ProductDto()
                    {
                        ProductId = product.ProductId,
                        Name = product.Name,
                        Description = product.Description,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price,
                        Status = product.Status,
                        CreatedAt = product.CreatedAt,
                        Category = product.Category
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// get products by category id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("products/{categoryId:Guid}")]
        public async Task<IActionResult> GetProductsByCategoryId([FromRoute] Guid categoryId)
        {
            try
            {
                var products = await productService.GetProductsByCategoryIdAsync(categoryId);
                if (products == null)
                {
                    return NotFound();
                }
                else
                {
                    List<ProductDto> result = new List<ProductDto>();
                    foreach (Product product in products)
                    {
                        result.Add(
                            new ProductDto()
                            {
                                ProductId = product.ProductId,
                                Name = product.Name,
                                Description = product.Description,
                                ImageUrl = product.ImageUrl,
                                Price = product.Price,
                                Status = product.Status,
                                CreatedAt = product.CreatedAt,
                                Category = product.Category
                            }
                            );
                    }
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// add's a new product to db
        /// </summary>
        /// <param name="product"></param>
        /// <returns>new product</returns>
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided!");
            }

            try
            {
                var newProduct = new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,
                    Status = "In Stock",
                    CreatedAt = DateTime.UtcNow,
                    CategoryId = product.CategoryId,
                    Category = null,
                };

                var existingCategory = await categoryService.GetCategoryByIdAsync(product.CategoryId);
                if (existingCategory != null)
                {
                    newProduct.Category = existingCategory;
                }

                newProduct = await productService.CreateProductAsync(newProduct);
                return Ok(newProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// updates the existing product in db
        /// </summary>
        /// <param name="updatedProduct"></param>
        /// <returns>updated product</returns>
        [HttpPut]
        [Route("{productId:Guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid productId, [FromBody] UpdateProductDto product)
        {
            try
            {
                var newProduct = new UpdateProductDto()
                {
                    Name = product.Name,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,
                    Status = product.Status,
                    ModifiedAt = DateTime.UtcNow,
                    CategoryId = product.CategoryId,
                    Category = null,
                };

                var existingCategory = await categoryService.GetCategoryByIdAsync(product.CategoryId);
                if (existingCategory != null)
                {
                    newProduct.Category = existingCategory;
                }

                var res = await productService.UpdateProductAsync(productId, newProduct);

                if (res == null)
                {
                    return NotFound();
                }
                else
                {
                    var result = new ProductDto()
                    {
                        ProductId = res.ProductId,
                        Name = res.Name,
                        Description = res.Description,
                        ImageUrl = res.ImageUrl,
                        Price = res.Price,
                        Status = res.Status,
                        CreatedAt = res.CreatedAt,
                        Category = res.Category,
                    };
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// delete a product in db based on id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>bool representing state of operation</returns>
        [HttpDelete]
        [Route("{productId:Guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid productId)
        {
            try
            {
                var deleteStatus = await productService.DeleteProductAsync(productId);
                return Ok(deleteStatus);
            }
            catch (InvalidDeletionException de)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}