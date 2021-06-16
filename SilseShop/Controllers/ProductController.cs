using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SilseShop.Database;
using SilseShop.Models;
using SilseShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilseShop.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository _products;
        private ShopCartManager _cartManager;
        public ProductController(ProductRepository productRepository, ShopCartManager cartManager)
        {
            _products = productRepository;
            _cartManager = cartManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            await _cartManager.AddToCart(_products.Get(id));
            return RedirectToAction("ShopCart", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFromCart(int id)
        {
            await _cartManager.DeleteFromCart(_products.Get(id));
            return RedirectToAction("ShopCart", "Product");
        }

        [HttpGet]
        public IActionResult ShopCart()
        {
            ShopCart cart = _cartManager.GetCart();
            List<ProductViewModel> shopCartItems = _cartManager.GetShopCartItems(cart.Id);
            return View(shopCartItems);
        }

    }
}
