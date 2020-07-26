using Microsoft.EntityFrameworkCore;
using SilseShop.Models;

namespace SilseShop.Database
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ShopCart> Carts { get; set; }
        public DbSet<ShopCartItem> ShopCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ShopCartItem>()
            .Property(i => i.Id)
            .ValueGeneratedOnAdd();
        }

    }
}
