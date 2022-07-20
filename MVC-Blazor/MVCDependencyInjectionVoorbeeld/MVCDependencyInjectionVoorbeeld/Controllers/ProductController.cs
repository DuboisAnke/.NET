using Microsoft.AspNetCore.Mvc;
using MVCDependencyInjectionVoorbeeld.Entities;
using MVCDependencyInjectionVoorbeeld.Services;

namespace MVCDependencyInjectionVoorbeeld.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repo;

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View(_repo.Products);
        }

        public IActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _repo.Add(product);
            return RedirectToAction("Index", "Product");
        }
    }
}
