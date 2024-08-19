using Microsoft.EntityFrameworkCore;
using ArtGalleryAPI.Models.Domain;

namespace ArtGalleryAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
    }
}
