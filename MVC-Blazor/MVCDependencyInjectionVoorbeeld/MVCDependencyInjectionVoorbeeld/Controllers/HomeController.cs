using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCDependencyInjectionVoorbeeld.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MVCDependencyInjectionVoorbeeld.Services;

namespace MVCDependencyInjectionVoorbeeld.Controllers
{
    public class HomeController : Controller
    {

        private IProductRepository _repo;
        public HomeController(IProductRepository repo)
        {
           _repo = repo;
        }

        public IActionResult Index()
        {
            ViewBag.ProductCount = _repo.Products.Count();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
