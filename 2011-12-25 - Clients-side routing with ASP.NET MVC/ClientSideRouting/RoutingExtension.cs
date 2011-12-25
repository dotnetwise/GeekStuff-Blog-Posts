using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace ClientSideRouting
{
    public static class RoutingExtension
    {
        public static void AddRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            Route route = routes.MapRoute(name, url, defaults);
            route.DataTokens.Add("RouteName", name);
        }
    }
}