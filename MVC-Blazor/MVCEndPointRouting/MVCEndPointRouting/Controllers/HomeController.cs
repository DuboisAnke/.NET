using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCEndPointRouting.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MVCEndPointRouting.Services;
using MVCEndPointRouting.ViewModels;

namespace MVCEndPointRouting.Controllers
{
    public class HomeController : Controller
    {
        IRoutingRepository _routingRepository;

        public HomeController(IRoutingRepository routingRepository)
        {
            _routingRepository = routingRepository;
        }

        public IActionResult Index()
        {
            RoutingViewModel routingViewModel = new RoutingViewModel("Home");
            routingViewModel.ActionName = "Index";
            _routingRepository.Add(routingViewModel);
            return View();
        }

        public IActionResult RoutingList()
        {
            RoutingViewModel routingViewModel = new RoutingViewModel("Home");
            routingViewModel.ActionName = "RoutingList";
            _routingRepository.Add(routingViewModel);
            return View(_routingRepository.RoutingViewModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
