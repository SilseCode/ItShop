using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SilseShop.Models;
using SilseShop.Services;

namespace SilseShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ProductRepository _productRepository;
        public HomeController(ILogger<HomeController> logger, ProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }
        
        public IActionResult Index()
        {
            return View(_productRepository.GetList());
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
