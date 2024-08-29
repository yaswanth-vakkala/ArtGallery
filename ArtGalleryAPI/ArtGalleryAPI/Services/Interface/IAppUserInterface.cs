using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;

namespace ArtGalleryAPI.Services.Interface
{
    public interface IAppUserInterface
    {
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<AppUser>? GetUserByIdAsync(string userId);
        Task<AppUser>? GetUserByEmailAsync(string email);
        Task<AppUser>? UpdateUserAsync(string userId, UpdateAppUserDto updatedUser);
        Task<bool> DeleteUserAsync(string userId);
    }
}
