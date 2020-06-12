using Microsoft.EntityFrameworkCore;
using SilseShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilseShop.Database
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        { 
        }
        public DbSet<Product> Products { get; set; }
    }
}
