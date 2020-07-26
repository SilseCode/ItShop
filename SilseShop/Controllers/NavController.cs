using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using SilseShop.Database;
using SilseShop.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SilseShop.Controllers
{

    public class NavController : Controller
    {
        private readonly ILogger<NavController> _logger;
        private ProductRepository _productRepository;
        private ShopDbContext _db;
        private List<ProductType> _productTypes;
        public NavController(ILogger<NavController> logger, ProductRepository productRepository, ShopDbContext db)
        {
            _logger = logger;
            _productRepository = productRepository;
            _db = db;
            _productTypes = _db.ProductTypes.ToList();
        }

        public IActionResult Index()
        {
            List<ProductViewModel> products = _productRepository.GetList().Select(p => new ProductViewModel(p.Id, p.Name, p.Price, p.ImgUrl, _productTypes.Single(t => t.Id == p.TypeId).Type)).ToList();
            return View(products);
        }

        [Route("search")]
        [HttpPost]
        public IActionResult Search(string searchRequest)
        {
            IEnumerable<string> words = searchRequest
                .Split(' ')
                .Select(w => w.ToLower());
            List<Product> preResult = _productRepository
                .GetList()
                .Where(p => p.Name.Split(' ').Any(n => words.Any(w => w.ToLower() == n.ToLower())))
                .ToList();
            List<ProductViewModel> products = preResult
                .Select(r => new ProductViewModel(r.Id, r.Name, r.Price, r.ImgUrl, _productTypes.Single(t => t.Id == r.TypeId).Type))
                .ToList();
            return View("Index", products);
        }
        [Route("processors")]
        public IActionResult Processors()
        {
            List<ProductViewModel> products = _productRepository.GetList()
                .Where(p => p.TypeId == 1)
                .Select(p => new ProductViewModel(p.Id, p.Name, p.Price, p.ImgUrl, _productTypes.Single(t => t.Id == p.TypeId).Type))
                .ToList();
            return View("Index", products);
        }

        [Route("videocards")]
        public IActionResult Videocards()
        {
            List<ProductViewModel> products = _productRepository
                .GetList()
                .Where(p => p.TypeId == 2)
                .Select(p => new ProductViewModel(p.Id, p.Name, p.Price, p.ImgUrl, _productTypes.Single(t => t.Id == p.TypeId).Type))
                .ToList();
            return View("Index", products);
        }

        [Route("motherboards")]
        public IActionResult Motherboards()
        {
            List<ProductViewModel> products = _productRepository
                .GetList()
                .Where(p => p.TypeId == 3)
                .Select(p => new ProductViewModel(p.Id, p.Name, p.Price, p.ImgUrl, _productTypes.Single(t => t.Id == p.TypeId).Type))
                .ToList();
            return View("Index", products);
        }

        [Route("ram")]
        public IActionResult RAM()
        {
            List<ProductViewModel> products = _productRepository
                .GetList()
                .Where(p => p.TypeId == 4)
                .Select(p => new ProductViewModel(p.Id, p.Name, p.Price, p.ImgUrl, _productTypes.Single(t => t.Id == p.TypeId).Type)).ToList();
            return View("Index", products);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
