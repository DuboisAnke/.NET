using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace MVCEndPointRouting.CustomConstraint
{
    public class FirstNameConstraint : IRouteConstraint
    {
        // this constraint makes sure only the names within this array are useable for endpoint routing, you can also add id here or anything else
        // So for instance you could find all the ids in the context and add them to the array, but only if nothing gets added anymore, since this service is a singleton
        // potentially this works with scoped?

        private string[] firstNames = new[] { "Kristof", "Wesley" };
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return firstNames.Contains(values[routeKey]);
        }
    }
}
