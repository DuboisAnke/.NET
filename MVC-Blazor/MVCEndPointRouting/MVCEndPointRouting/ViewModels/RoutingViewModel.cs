using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace MVCEndPointRouting.ViewModels
{
    public class RoutingViewModel
    {
        public string Controller { get; set; }
        public DateTime Date { get; set; }

        public RoutingViewModel(string controller)
        {
            Controller = controller;
            Date = DateTime.Now;
        }

        public string ActionName { get; set; }
        public Dictionary<string, string> RoutingParameters = new Dictionary<string, string>();
        public string Parameters => string.Join(',', RoutingParameters.Select(x => string.Format("{0}:{1}", x.Key, x.Value)));
    }
}
