using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryInterface categoryService;
        public CategoryController(ICategoryInterface categoryService)
        {
            this.categoryService = categoryService;
        }

        /// <summary>
        /// returns all the categories from the database
        /// </summary>
        /// <returns>list of all categories</returns>

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await categoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// returns the filtered category record based on id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>filtered category</returns>
        [HttpGet]
        [Route("{categoryId:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid categoryId)
        {
            try
            {
                var category = await categoryService.GetCategoryByIdAsync(categoryId);
                if (category == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(category);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// add's a new category to db
        /// </summary>
        /// <param name="category"></param>
        /// <returns>new category</returns>
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided!");
            }

            try
            {
                var newCategory = new Category
                {
                    Name = category.Name,
                    Description = category.Description,
                    CreatedAt = DateTime.UtcNow,
                };
                await categoryService.CreateCategoryAsync(newCategory);
                var locationUri = Url.Action("GetCategoryById", new { categoryId = newCategory.CategoryId });
                return Created(locationUri, newCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// updates the existing category in db
        /// </summary>
        /// <param name="updatedCategory"></param>
        /// <returns>updated category</returns>
        [HttpPut]
        [Route("{categoryId:Guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid categoryId, [FromBody] UpdateCategoryDto updatedCategory)
        {
            try
            {
                var result = await categoryService.UpdateCategoryAsync(categoryId, updatedCategory);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// delete a category in db based on id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>bool representing state of operation</returns>
        [HttpDelete]
        [Authorize(Roles = "Writer")]
        [Route("{categoryId:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
        {
            try
            {
                var deleteStatus = await categoryService.DeleteCategoryAsync(categoryId);
                return Ok(deleteStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
