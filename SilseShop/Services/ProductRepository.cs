using SilseShop.Database;
using SilseShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilseShop.Models
{
    public class ProductRepository : IRepository<Product>
    {
        private ShopDbContext _dbContext;
        public ProductRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(Product item)
        {
            _dbContext.Products.Add(item);
        }

        public void Delete(int id)
        {
            var product = _dbContext.Products.Find(id);
            if(product!= null)
            {
                _dbContext.Products.Remove(product);
            }
        }


        public Product Get(int id)
        {
            return _dbContext.Products.Find(id);
        }

        public IEnumerable<Product> GetList()
        {
            return _dbContext.Products;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Product item)
        {
            _dbContext.Products.Update(item);
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
