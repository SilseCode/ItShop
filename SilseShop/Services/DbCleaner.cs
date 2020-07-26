using Microsoft.EntityFrameworkCore.ChangeTracking;
using SilseShop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace SilseShop.Services
{
    public class DbCleaner
    {
        private ShopDbContext _db;
        public DbCleaner(ShopDbContext db)
        {
            _db = db;
        }
        
        public async Task Clear()
        {
            
        }

    }
}
