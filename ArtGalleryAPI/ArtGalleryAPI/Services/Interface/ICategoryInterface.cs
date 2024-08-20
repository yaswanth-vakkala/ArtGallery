using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;

namespace ArtGalleryAPI.Services.Interface
{
    public interface ICategoryInterface
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category>? GetCategoryByIdAsync(Guid categoryId);
        Task<Category> CreateCategoryAsync(Category newCategory);
        Task<Category>? UpdateCategoryAsync(UpdateCategoryDto updatedCategory);
        Task<bool> DeleteCategoryAsync(Guid categoryId);
    }
}
