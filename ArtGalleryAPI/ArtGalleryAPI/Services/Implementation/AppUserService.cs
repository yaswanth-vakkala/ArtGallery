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

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync(int pageNumber, int pageSize, string? query = null, string? sortBy = null, string? sortOrder = null)
        {
            var skipResults = (pageNumber - 1) * pageSize;
            var users = dbcontext.Users.AsQueryable();

            if (string.IsNullOrWhiteSpace(query) == false)
            {
                users = users.Where(x => x.Email.Contains(query) || x.FirstName.Contains(query) || x.LastName.Contains(query));
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (string.Equals(sortBy, "Email", StringComparison.OrdinalIgnoreCase))
                {
                    var isDesc = string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    users = isDesc ? users.OrderByDescending(u => u.Email) : users.OrderBy(u => u.Email);
                }
                else if (string.Equals(sortBy, "FirstName", StringComparison.OrdinalIgnoreCase))
                {
                    var isDesc = string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    users = isDesc ? users.OrderByDescending(u => u.FirstName) : users.OrderBy(u => u.FirstName);
                }
            }

            users = users.Skip(skipResults).Take(pageSize);

            return await users.ToListAsync();
        }

        public async Task<AppUser>? GetUserByIdAsync(string userId)
        {
            var user = await dbcontext.Users.SingleOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<int> GetUserCountAsync(string? query)
        {
            var userCount = 0;
            if (!string.IsNullOrWhiteSpace(query))
            {
                userCount = await dbcontext.Users.Where(u => u.FirstName.Contains(query) || u.Email.Contains(query)).CountAsync();
            }
            else
            {
                userCount = await dbcontext.Users.CountAsync();
            }
            return userCount;
        }

        public async Task<AppUser>? GetUserByEmailAsync(string email)
        {
            var user = await dbcontext.Users.SingleOrDefaultAsync(u => u.Email == email);
            return user;
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
