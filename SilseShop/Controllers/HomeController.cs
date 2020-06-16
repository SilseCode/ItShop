using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SilseShop.Database;
using SilseShop.Models;
using SilseShop.Services;

namespace SilseShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ProductRepository _productRepository;
        private ShopDbContext _db;
        private List<ProductType> _productTypes;
        public HomeController(ILogger<HomeController> logger, ProductRepository productRepository, ShopDbContext db)
        {
            _logger = logger;
            _productRepository = productRepository;
            _db = db;
            _productTypes = _db.ProductTypes.ToList();
        }

        public IActionResult Index()
        { 

            List<ProductViewModel> products = _productRepository.GetList().Select(p => new ProductViewModel(p.Name, p.Price, p.ImgUrl, _productTypes.Single(t=>t.Id == p.TypeId).Type)).ToList(); 
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string searchRequest)
        {
            return StatusCode(200);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
