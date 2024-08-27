using ArtGalleryAPI.CustomExceptions;
using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Azure;
using Azure.Core;
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
                            Categories = product.Categories.Select(x => new Category
                            {
                                CategoryId = x.CategoryId,
                                Name = x.Name,
                                Description = x.Description,
                                CreatedAt = x.CreatedAt,
                            }).ToList()
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
                        Categories = product.Categories.Select(x => new Category
                        {
                            CategoryId = x.CategoryId,
                            Name = x.Name,
                            Description = x.Description,
                            CreatedAt = x.CreatedAt,
                        }).ToList()
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
                                Categories = product.Categories.Select(x => new Category
                                {
                                    CategoryId = x.CategoryId,
                                    Name = x.Name,
                                    Description = x.Description,
                                    CreatedAt = x.CreatedAt,
                                }).ToList()
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
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow,
                    Quantity = product.Quantity,
                    Categories = new List<Category>()
                };

                foreach (var categoryGuid in product.Categories)
                {
                    var existingCategory = await categoryService.GetCategoryByIdAsync(categoryGuid);
                    if (existingCategory != null)
                    {
                        newProduct.Categories.Add(existingCategory);
                    }
                }

                newProduct = await productService.CreateProductAsync(newProduct);

                var response = new ProductDto()
                {
                    ProductId = newProduct.ProductId,
                    Name = newProduct.Name,
                    Description = newProduct.Description,
                    ImageUrl = newProduct.ImageUrl,
                    Price = newProduct.Price,
                    Status = newProduct.Status,
                    CreatedAt = newProduct.CreatedAt,
                    Categories = newProduct.Categories.Select(x => new Category
                    {
                        CategoryId = x.CategoryId,
                        Name = x.Name,
                        Description = x.Description,
                        CreatedAt = x.CreatedAt,
                    }).ToList()
                };
                return Ok(response);
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
                var newProduct = new Product
                {
                    ProductId = productId,
                    Name = product.Name,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,
                    Status = product.Status,
                    Quantity = product.Quantity,
                    Categories = new List<Category>()
                };

                foreach (var categoryGuid in product.Categories)
                {
                    var existingCategory = await categoryService.GetCategoryByIdAsync(categoryGuid);
                    if (existingCategory != null)
                    {
                        newProduct.Categories.Add(existingCategory);
                    }
                }


                newProduct = await productService.UpdateProductAsync(productId, newProduct);

                if (newProduct == null)
                {
                    return NotFound();
                }
                else
                {
                    var response = new ProductDto()
                    {
                        ProductId = newProduct.ProductId,
                        Name = newProduct.Name,
                        Description = newProduct.Description,
                        ImageUrl = newProduct.ImageUrl,
                        Price = newProduct.Price,
                        Status = newProduct.Status,
                        CreatedAt = newProduct.CreatedAt,
                        Categories = newProduct.Categories.Select(x => new Category
                        {
                            CategoryId = x.CategoryId,
                            Name = x.Name,
                            Description = x.Description,
                            CreatedAt = x.CreatedAt,
                        }).ToList()
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