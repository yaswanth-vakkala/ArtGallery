using ArtGalleryAPI.CustomExceptions;
using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
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
        public async Task<IActionResult> GetAllProducts([FromQuery] string? query, [FromQuery] string? sortBy, [FromQuery] string? sortOrder
            ,[FromQuery] int pageNumber=1, [FromQuery] int pageSize=8)
        {
            try
            {
                var products = await productService.GetAllProductsAsync(pageNumber, pageSize, query, sortBy, sortOrder);
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
        /// get count of the total proucts
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetProductsCount([FromQuery] string query=null)
        {
            try
            {
                var productCount = await productService.GetProductsCountAsync(query);
                return Ok(productCount);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
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
        /// returns the filtered product record based on id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>filtered product</returns>
        [HttpPost]
        [Route("cart")]
        [Authorize]
        public async Task<IActionResult> GetProductsFromIdArray([FromBody] ProductsIdListRequest productIds)
        {
            try
            {
                if (!productIds.productIds.Any())
                {
                    return NotFound();
                }
                var products = await productService.GetProductsFromIdArrayAsync(productIds.productIds);
                if (products == null || !products.Any())
                {
                    return NotFound();
                }
                else
                {
                    /*var response = new ProductDto()
                    {
                        ProductId = product.ProductId,
                        Name = product.Name,
                        Description = product.Description,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price,
                        Status = product.Status,
                        CreatedAt = product.CreatedAt,
                        Category = product.Category
                    };*/
                    return Ok(products);
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
        [Authorize(Roles ="Writer")]
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


        [HttpPost]
        [Route("addbulk")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> AddBulkProductsForAdmin([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file!");
            }

            const long maxFileSize = 4 * 1024 * 1024;
            if (file.Length > maxFileSize)
            {
                return BadRequest("File size exceeds limit!");
            }

            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (fileExtension != ".xlsx" && fileExtension != ".xls")
            {
                return BadRequest("Invalid file!");
            }

            List<string> expectedHeaders = new List<string>() { "name", "description", "imageurl", "price", "categoryid"};
            List<string> actualHeaders = new List<string>();
            bool areHeadersRead = false;
            List<AddBulkProductsResponseDto> addBulkProductsResponse = new List<AddBulkProductsResponseDto>();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (
                    var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration()
                    {
                        LeaveOpen = false,
                        AutodetectSeparators = new char[] { ',', ';', '\t', '#', '|' },
                    })
                    )
                {
                    while (reader.Read())
                    {
                        var res = new AddBulkProductsResponseDto();
                        if ((String.IsNullOrWhiteSpace(reader.GetString(0)) || reader.RowCount < 2) && areHeadersRead == false)
                        {
                            return BadRequest("Invalid file!");
                        }
                        if (areHeadersRead == false)
                        {
                            if (reader.FieldCount == expectedHeaders.Count)
                            {
                                actualHeaders.Add(reader.GetString(0).ToLower());
                                actualHeaders.Add(reader.GetString(1).ToLower());
                                actualHeaders.Add(reader.GetString(2).ToLower());
                                actualHeaders.Add(reader.GetString(3).ToLower());
                                actualHeaders.Add(reader.GetString(4).ToLower());
                                areHeadersRead = true;
                            }
                            if (!actualHeaders.SequenceEqual(expectedHeaders))
                            {
                                return BadRequest("Invalid file!");
                            }
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("name"))) &&
                           string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("description"))) &&
                           string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("imageurl"))) &&
                           (reader.GetDouble(actualHeaders.IndexOf("price")) == null || reader.GetDouble(actualHeaders.IndexOf("price")) < 1) &&
                           string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("categoryid"))))
                        {
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("name"))) ||
                            string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("description"))) ||
                            string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("imageurl")))||
                           (reader.GetDouble(actualHeaders.IndexOf("price")) == null || reader.GetDouble(actualHeaders.IndexOf("price")) < 1) &&
                            string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("categoryid")))
                            )
                        {
                            res.Status = "failed";
                            res.ErrorInfo = "mandatory fields are not filled";
                            addBulkProductsResponse.Add(res);
                            continue;
                        }

                        res.Name = reader.GetString(actualHeaders.IndexOf("name"));
                        res.Description = reader.GetString(actualHeaders.IndexOf("description"));
                        res.ImageUrl = reader.GetString(actualHeaders.IndexOf("imageurl"));
                        res.Price = reader.GetDouble(actualHeaders.IndexOf("price"));
                        res.Categoryid = new Guid(reader.GetString(actualHeaders.IndexOf("categoryid")));

                        var product = new Product()
                        {
                            Name = reader.GetString(actualHeaders.IndexOf("name")),
                            Description = reader.GetString(actualHeaders.IndexOf("description")),
                            ImageUrl = reader.GetString(actualHeaders.IndexOf("imageurl")),
                            Price = (decimal)reader.GetDouble(actualHeaders.IndexOf("price")),
                            CategoryId = new Guid(reader.GetString(actualHeaders.IndexOf("categoryid"))),
                            Status = "Active",
                            CreatedAt = DateTime.UtcNow,
                        };
                        try
                        {
                            await productService.CreateProductAsync(product);
                            res.Status = "success";
                        }catch (Exception ex)
                        {
                            res.ErrorInfo = ex.Message;
                            res.Status = "failed";
                        }
                        finally
                        {
                            addBulkProductsResponse.Add(res);
                        }
                    }
                }
            }
            return Ok(addBulkProductsResponse);
        }

        /// <summary>
        /// updates the existing product in db
        /// </summary>
        /// <param name="updatedProduct"></param>
        /// <returns>updated product</returns>
        [HttpPut]
        [Route("{productId:Guid}")]
        [Authorize(Roles = "Writer")]
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
                    //Category = null,
                };

                /*var existingCategory = await categoryService.GetCategoryByIdAsync(product.CategoryId);
                if (existingCategory != null)
                {
                    newProduct.Category = existingCategory;
                }*/

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
                        //Category = res.Category,
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
        [Authorize]
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

        /// <summary>
        /// delete product's in db based on id's
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns>bool representing state of operation</returns>
        [HttpPost]
        [Route("deleteproducts")]
        [Authorize]
        public async Task<IActionResult> DeleteProducts([FromBody] Guid[] productIds)
        {
            try
            {
                var deleteStatus = await productService.DeleteProductsAsync(productIds);
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