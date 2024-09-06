using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;

namespace ArtGalleryAPI.Services.Interface
{
    public interface IAppUserInterface
    {
        Task<IEnumerable<AppUser>> GetAllUsersAsync(int pageNumber, int pageSize, string? query = null, string? sortBy = null, string? sortOrder = null);
        Task<AppUser>? GetUserByIdAsync(string userId);
        Task<int>? GetUserCountAsync(string? query);
        Task<AppUser>? GetUserByEmailAsync(string email);
        Task<AppUser>? UpdateUserAsync(string userId, UpdateAppUserDto updatedUser);
        Task<bool> DeleteUserAsync(string userId);
    }
}
