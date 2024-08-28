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
        public DbSet<Category> Category { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<AppOrder> AppOrder { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<Payment> Payment { get; set; }
    }
}
