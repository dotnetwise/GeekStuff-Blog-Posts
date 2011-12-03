using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace SubdomainRouting.Models
{
    public class SubdomainRoute : Route, IRouteWithArea
    {
        private string[] namespaces;
        public string Subdomain { get; private set; }

        public SubdomainRoute(string subdomain, string url, object defaults, string[] namespaces)
            : base(url, new RouteValueDictionary(defaults), new MvcRouteHandler())
        {
            this.Subdomain = subdomain;
            this.namespaces = namespaces;
        }

        public string Area
        {
            get { return Convert.ToString(this.Defaults["area"]); }
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            string requestPath = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;
            string requestDomain = GetSubdomain(httpContext.Request.Headers["Host"]);

            RouteData data = null;
            Regex domainRegex = CreateRegex(this.Subdomain);
            Regex pathRegex = CreateRegex(this.Url);
            Match domainMatch = domainRegex.Match(requestDomain);
            Match pathMatch = pathRegex.Match(requestPath);

            if (domainMatch.Success && pathMatch.Success)
            {
                data = new RouteData(this, RouteHandler);

                if (Defaults != null)
                {
                    foreach (KeyValuePair<string, object> item in Defaults)
                        data.Values[item.Key] = item.Value;
                }

                for (int i = 0; i < pathMatch.Groups.Count; i++)
                {
                    Group group = pathMatch.Groups[i];
                    if (group.Success)
                    {
                        string key = pathRegex.GroupNameFromNumber(i);

                        if (!string.IsNullOrEmpty(key) && !char.IsNumber(key, 0))
                        {
                            if (!string.IsNullOrEmpty(group.Value))
                            {
                                data.Values[key] = group.Value;
                            }
                        }
                    }
                }

                data.DataTokens.Add("Area", data.Values["area"]);
                data.DataTokens.Add("namespaces", this.namespaces);
            }

            return data;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return base.GetVirtualPath(requestContext, values);
        }

        public static string GetSubdomain(string host)
        {
            if (host.IndexOf(":") >= 0)
                host = host.Substring(0, host.IndexOf(":"));

            Regex tldRegex = new Regex(@"\.[a-z]{2,3}\.[a-z]{2}$");
            host = tldRegex.Replace(host, "");
            tldRegex = new Regex(@"\.[a-z]{2,4}$");
            host = tldRegex.Replace(host, "");

            if (host.Split('.').Length > 1)
                return host.Substring(0, host.IndexOf("."));
            else
                return string.Empty;
        }

        private Regex CreateRegex(string source)
        {
            source = source.Replace("/", @"\/?");
            source = source.Replace(".", @"\.?");
            source = source.Replace("-", @"\-?");
            source = source.Replace("{", @"(?<");
            source = source.Replace("}", @">([a-zA-Z0-9_-]*))");
            return new Regex("^" + source + "$");
        }
    }
}