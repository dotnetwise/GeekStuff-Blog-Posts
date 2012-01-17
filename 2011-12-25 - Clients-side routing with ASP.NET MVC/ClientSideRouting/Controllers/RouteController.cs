using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;
using System.Text;

namespace ClientSideRouting.Controllers
{
    public class RouteController : Controller
    {
        [HttpGet]
        public ActionResult List()
        {
            JavascriptRouteGenerator generator = new JavascriptRouteGenerator();
            StringBuilder js = new StringBuilder();

            string baseUrl = UrlHelper.GenerateContentUrl("~/", this.HttpContext);
            js.AppendFormat("var baseUrl = '{0}';", baseUrl).AppendLine();

            foreach (Route route in RouteTable.Routes)
            {
                if (route.RouteHandler is MvcRouteHandler)
                {
                    string name = Convert.ToString(route.DataTokens["RouteName"]);
                    string url = route.Url;
                    string routeJs = generator.Build(name, url, route.Defaults);
                    js.Append(routeJs).AppendLine();

                }
            }
            return new JavaScriptResult(js);
        }
    }
}
