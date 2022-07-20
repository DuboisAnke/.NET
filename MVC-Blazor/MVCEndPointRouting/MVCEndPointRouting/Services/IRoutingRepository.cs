using System.Collections;
using System.Collections.Generic;
using MVCEndPointRouting.ViewModels;

namespace MVCEndPointRouting.Services
{
    public interface IRoutingRepository
    {
        IEnumerable<RoutingViewModel> RoutingViewModels { get; }
        void Add(RoutingViewModel model);

    }
}
