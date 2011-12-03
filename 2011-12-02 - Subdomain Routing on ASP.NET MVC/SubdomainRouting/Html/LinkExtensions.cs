using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.Mvc;
using SubdomainRouting.Models;

namespace SubdomainRouting.Html
{
    public static class LinkExtensions
    {
        public static IHtmlString AdvRouteLink(this HtmlHelper htmlHelper, string linkText, string routeName)
        {
            return htmlHelper.AdvRouteLink(linkText, routeName, new { }, new { });
        }

        public static IHtmlString AdvRouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues)
        {
            return htmlHelper.AdvRouteLink(linkText, routeName, routeValues, new { });
        }

        public static IHtmlString AdvRouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues, object htmlAttributes)
        {
            RouteValueDictionary routeValueDict = new RouteValueDictionary(routeValues);
            var request = htmlHelper.ViewContext.RequestContext.HttpContext.Request;
            string host = request.IsLocal ? request.Headers["Host"] : request.Url.Host;
            if (host.IndexOf(":") >= 0)
                host = host.Substring(0, host.IndexOf(":"));

            string url = UrlHelper.GenerateUrl(routeName, null, null, routeValueDict, RouteTable.Routes, htmlHelper.ViewContext.RequestContext, false);
            var virtualPathData = RouteTable.Routes.GetVirtualPathForArea(htmlHelper.ViewContext.RequestContext, routeName, routeValueDict);

            var route = virtualPathData.Route as SubdomainRoute;

            string actualSubdomain = SubdomainRoute.GetSubdomain(host);
            if (!string.IsNullOrEmpty(actualSubdomain))
                host = host.Substring(host.IndexOf(".") + 1);

            if (!string.IsNullOrEmpty(route.Subdomain))
                host = string.Concat(route.Subdomain, ".", host);
            else
                host = host.Substring(host.IndexOf(".") + 1);

            UriBuilder builder = new UriBuilder(request.Url.Scheme, host, 80, url);

            if (request.IsLocal)
                builder.Port = request.Url.Port;

            url = builder.Uri.ToString();

            return htmlHelper.Link(linkText, url, htmlAttributes);
        }

        private static IHtmlString Link(this HtmlHelper htmlHelper, string text, string url, object htmlAttributes)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.Attributes.Add("href", url);
            tag.InnerHtml = text;
            tag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
    }
}