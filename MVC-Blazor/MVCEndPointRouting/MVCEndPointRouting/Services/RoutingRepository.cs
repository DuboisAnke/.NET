using MVCEndPointRouting.ViewModels;
using System.Collections.Generic;

namespace MVCEndPointRouting.Services
{
    public class RoutingRepository : IRoutingRepository
    {
        private List<RoutingViewModel> _routingViewModels = new List<RoutingViewModel>();
        public IEnumerable<RoutingViewModel> RoutingViewModels => _routingViewModels;
        public void Add(RoutingViewModel model)
        {
            _routingViewModels.Add(model);
        }
    }
}
