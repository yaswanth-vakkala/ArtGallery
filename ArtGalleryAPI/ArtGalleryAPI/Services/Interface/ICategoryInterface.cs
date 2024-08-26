using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;

namespace ArtGalleryAPI.Services.Interface
{
    public interface ICategoryInterface
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category>? GetCategoryByIdAsync(Guid categoryId);
        Task<Category> CreateCategoryAsync(Category newCategory);
        Task<Category>? UpdateCategoryAsync(Guid categoryId,UpdateCategoryDto updatedCategory);
        Task<bool> DeleteCategoryAsync(Guid categoryId);
    }
}
