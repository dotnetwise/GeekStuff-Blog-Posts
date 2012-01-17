using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class RoutingExtension
    {
    	public static Route AddRoute(this RouteCollection routes, string name, string url)
		{
			Route route = routes.MapRoute(name, url);
            route.DataTokens.Add("RouteName", name);
			return route;
		}
		public static Route AddRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            Route route = routes.MapRoute(name, url, defaults);
            route.DataTokens.Add("RouteName", name);
			return route;
        }
		public static Route AddRoute(this RouteCollection routes, string name, string url, string[] namespaces)
		{
            Route route = routes.MapRoute(name, url, namespaces);
            route.DataTokens.Add("RouteName", name);
			return route;
        }
		public static Route AddRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
		{
            Route route = routes.MapRoute(name, url, defaults, constraints);
            route.DataTokens.Add("RouteName", name);
			return route;
        }
		public static Route AddRoute(this RouteCollection routes, string name, string url, object defaults, string[] namespaces)
		{
            Route route = routes.MapRoute(name, url, defaults, namespaces);
            route.DataTokens.Add("RouteName", name);
			return route;
        }
		public static Route AddRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
		{
            Route route = routes.MapRoute(name, url, defaults, constraints, namespaces);
            route.DataTokens.Add("RouteName", name);
			return route;
        }

		public static Route AddRoute(this AreaRegistrationContext context, string name, string url)
		{
			Route route = context.MapRoute(name, url);
			route.DataTokens.Add("RouteName", name);
			return route;
		}
		public static Route AddRoute(this AreaRegistrationContext context, string name, string url, object defaults)
		{
			Route route = context.MapRoute(name, url, defaults);
			route.DataTokens.Add("RouteName", name);
			return route;
		}
		public static Route AddRoute(this AreaRegistrationContext context, string name, string url, string[] namespaces)
		{
			Route route = context.MapRoute(name, url, namespaces);
			route.DataTokens.Add("RouteName", name);
			return route;
		}
		public static Route AddRoute(this AreaRegistrationContext context, string name, string url, object defaults, object constraints)
		{
			Route route = context.MapRoute(name, url, defaults, constraints);
			route.DataTokens.Add("RouteName", name);
			return route;
		}
		public static Route AddRoute(this AreaRegistrationContext context, string name, string url, object defaults, string[] namespaces)
		{
			Route route = context.MapRoute(name, url, defaults, namespaces);
			route.DataTokens.Add("RouteName", name);
			return route;
		}
		public static Route AddRoute(this AreaRegistrationContext context, string name, string url, object defaults, object constraints, string[] namespaces)
		{
			Route route = context.MapRoute(name, url, defaults, constraints, namespaces);
			route.DataTokens.Add("RouteName", name);
			return route;
		}
    }
}