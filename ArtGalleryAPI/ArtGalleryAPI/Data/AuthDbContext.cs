using ArtGalleryAPI.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Data
{
    public class AuthDbContext : IdentityDbContext<AppUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // create reader, writer roles
            var readerRoleId = "5cfe7d19-a05b-4b48-a4e4-eebd9f40529f";
            var writerRoleId = "c91a3a78-4cee-4fba-8f81-46bb5280a56b";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            // seed roles
            builder.Entity<IdentityRole>().HasData(roles);

            // create admin user
            var adminUserId = "4f3cd3ce-b56e-46b4-85c1-9d6598915c81";
            var admin = new AppUser()
            {
                Id = adminUserId,
                UserName = "admin@galleria.com",
                Email = "admin@galleria.com",
                FirstName = "admin",
                NormalizedEmail = "admin@galleria.com".ToUpper(),
                NormalizedUserName = "admin@galleria.com".ToUpper(),
                Status = "Active",
                CreatedAt = DateTime.UtcNow
            };
            admin.PasswordHash = new PasswordHasher<AppUser>().HashPassword(admin, "admin@123");
            builder.Entity<AppUser>().HasData(admin);
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId= writerRoleId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
