using Microsoft.AspNetCore.Mvc;
using MVCEndPointRouting.Services;
using MVCEndPointRouting.ViewModels;

namespace MVCEndPointRouting.Controllers
{
    public class RoutingController : Controller
    {
        private IRoutingRepository _routingRepository;

        public RoutingController(IRoutingRepository routingRepository)
        {
            _routingRepository = routingRepository;
        }

        public IActionResult RoutingInt(string id)
        {
            RoutingViewModel routingViewModel = new RoutingViewModel("Routing");
            routingViewModel.ActionName = "RoutingInt";
            routingViewModel.RoutingParameters.Add("id", id);
            _routingRepository.Add(routingViewModel);
            ViewBag.ValueOfId = id ?? "Null Value";

            return View();
        }

        public IActionResult RoutingName(string id, string name)
        {
            RoutingViewModel routingViewModel = new RoutingViewModel("Routing");
            routingViewModel.ActionName = "RoutingInt";
            routingViewModel.RoutingParameters.Add("id", id);
            routingViewModel.RoutingParameters.Add("name", name);
            _routingRepository.Add(routingViewModel);
            ViewBag.ValueOfId = id ?? "Null Value";
            ViewBag.ValueOfName = name ?? "Name is empty";

            return View();
        }
    }
}
