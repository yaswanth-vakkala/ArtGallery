using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Services.Implementation
{
    public class AppUserService : IAppUserInterface
    {
        private readonly AuthDbContext dbcontext;

        public AppUserService(AuthDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            var users = await dbcontext.Users.ToListAsync();
            return users;
        }

        public async Task<AppUser>? GetUserByIdAsync(string userId)
        {
            var user = await dbcontext.Users.SingleOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<AppUser>? GetUserByEmailAsync(string email)
        {
            var user = await dbcontext.Users.SingleOrDefaultAsync(u => u.Email == email);
            return user;
        }
        public Task<AppUser> CreateUserAsync(AppUser appUser)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser>? UpdateUserAsync(string userId, UpdateAppUserDto updatedUser)
        {
            var user = await dbcontext.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }
            else
            {
                dbcontext.Entry(user).CurrentValues.SetValues(updatedUser);
                await dbcontext.SaveChangesAsync();
                return user;
            }
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await dbcontext.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return false;
            }
            else
            {
                dbcontext.Users.Remove(user);
                await dbcontext.SaveChangesAsync();
                return true;
            }
        }
    }
}
