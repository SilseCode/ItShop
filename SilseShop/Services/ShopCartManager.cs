
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SilseShop.Database;
using SilseShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilseShop.Services
{
    public class ShopCartManager
    {
        private ShopDbContext _db;
        private IServiceProvider _services;
        public ShopCartManager(ShopDbContext db, IServiceProvider services)
        {
            _db = db;
            _services = services;
        }

        public async Task AddToCart(Product product)
        {
            ShopCart cart = GetCart();
            if (_db.ShopCartItems.Any(i => i.Product.Id == product.Id && i.ShopCartId == cart.Id))
            {
                ShopCartItem item = _db.ShopCartItems.FirstOrDefault(i => i.Product.Id == product.Id && i.ShopCartId == cart.Id);
                item.Count++;
                _db.ShopCartItems.Update(item);
            }
            else
            {
                ShopCartItem shopCartItem = new ShopCartItem()
                {
                    ShopCart = cart,
                    ShopCartId = cart.Id,
                    Product = product,
                    Count = 1,
                };
                _db.ShopCartItems.Add(shopCartItem);
            }
            await _db.SaveChangesAsync();
        }

        public async Task DeleteFromCart(Product product)
        {
            ShopCart cart = GetCart();
            if (_db.ShopCartItems.Any(i => i.Product.Id == product.Id && i.ShopCartId == cart.Id))
            {
                ShopCartItem item = _db.ShopCartItems.FirstOrDefault(i => i.Product.Id == product.Id && i.ShopCartId == cart.Id);
                item.Count--;
                if (item.Count == 0)
                {
                    _db.ShopCartItems.Remove(item);
                }
                else
                {
                    _db.ShopCartItems.Update(item);
                }
                await _db.SaveChangesAsync();
            }
        }

        public List<ProductViewModel> GetShopCartItems(string cartId)
        {
            return _db.ShopCartItems
                 .Where(i => i.ShopCart.Id == cartId)
                 .Select(p => new ProductViewModel(p.Product) { Count = p.Count })
                 .ToList();
        }
        public ShopCart GetCart()
        {
            HttpContext httpContext = _services.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
            if (httpContext.Session.GetString("cart") == null)
            {
                ShopCart cart = new ShopCart();
                string cartId = Guid.NewGuid().ToString();
                cart.Id = cartId;
                cart.DateCreation = DateTime.Now;
                httpContext.Session.SetString("cart", cartId);
                _db.Carts.Add(cart);
                _db.SaveChanges();
                return cart;
            }
            else
            {
                string cartId = httpContext.Session.GetString("cart");
                ShopCart cart = _db.Carts.Find(cartId);
                if (cart == null)
                {
                    cart = new ShopCart();
                    cartId = Guid.NewGuid().ToString();
                    cart.Id = cartId;
                    cart.DateCreation = DateTime.Now;
                    httpContext.Session.SetString("cart", cartId);
                    _db.Carts.Add(cart);
                    _db.SaveChanges();
                    return cart;
                }
                return cart;
            }
        }
    }
}
