using Microsoft.AspNetCore.Mvc;

namespace PXLFunds.Controllers
{
    public class BlazorController : Controller
    {
        public IActionResult Index()
        {
            return View("_BlazorServer_Host");
        }
    }
}
