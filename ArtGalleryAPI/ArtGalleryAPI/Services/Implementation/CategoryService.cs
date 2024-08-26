using ArtGalleryAPI.CustomExceptions;
using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Services.Implementation
{
    public class CategoryService : ICategoryInterface
    {
        private readonly ApplicationDbContext dbContext;
        public CategoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categories = await dbContext.Category.ToListAsync();
            return categories;
        }

        public async Task<Category>? GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await dbContext.Category.SingleOrDefaultAsync(category => category.CategoryId == categoryId);
            return category;
        }

        public async Task<Category> CreateCategoryAsync(Category newCategory)
        {
            await dbContext.Category.AddAsync(newCategory);
            await dbContext.SaveChangesAsync();
            return newCategory;
        }

        public async Task<Category>? UpdateCategoryAsync(Guid categoryId,UpdateCategoryDto updatedCategory)
        {
            var category = await dbContext.Category.SingleOrDefaultAsync(category => category.CategoryId == categoryId);
            if (category == null)
            {
                return null;
            }
            else
            {
                dbContext.Entry(category).CurrentValues.SetValues(updatedCategory);
                await dbContext.SaveChangesAsync();
                return category;
            }
        }

        public async Task<bool> DeleteCategoryAsync(Guid categoryId)
        {
            var category = await dbContext.Category.SingleOrDefaultAsync(category => category.CategoryId == categoryId);
            if (category == null)
            {
                return false;
            }
            else
            {
                dbContext.Category.Remove(category);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
