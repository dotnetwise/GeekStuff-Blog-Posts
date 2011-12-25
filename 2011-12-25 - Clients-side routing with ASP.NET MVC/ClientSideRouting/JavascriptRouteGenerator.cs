using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;

namespace ClientSideRouting
{
    public class JavascriptRouteGenerator
    {
        public string Build(string name, string url)
        {
            return this.Build(name, url, new Dictionary<string, object>());
        }

        public string Build(string name, string url, IDictionary<string, object> defaults)
        {
            Regex r = new Regex("{(.*?)}");
            MatchCollection matches = r.Matches(url);
            string[] listOfParameters = matches.OfType<Match>().Select(x => x.Groups[1].Value).ToArray();
            if (listOfParameters.Length > 0)
            {
                string jsUrl = url;
                if (!jsUrl.StartsWith("{"))
                    jsUrl = jsUrl.Insert(0, "'");

                foreach (var parameter in listOfParameters)
                {
                    string replaceThis = string.Concat("{", parameter, "}");
                    string withThis = string.Concat("{params.", parameter, "}");
                    jsUrl = jsUrl.Replace(replaceThis, withThis);
                }

                jsUrl = jsUrl.Replace("}/", ",'/")
                             .Replace("{", "',")
                             .Replace("}", ",");

                if (jsUrl.StartsWith("',"))
                    jsUrl = jsUrl.Substring(2);

                if (jsUrl.EndsWith(","))
                    jsUrl = jsUrl.Substring(0, jsUrl.Length - 1);

                if (!url.EndsWith("}"))
                    jsUrl = jsUrl.Insert(jsUrl.Length, "'");

                string defaultsAsJson = this.GetDefaultsAsJson(defaults);
                return string.Format(@"Url.AddRoute('{0}', function(params) {{ return baseUrl.concat({1}); }}{2});", name, jsUrl, defaultsAsJson);
            }
            else
            {
                return string.Format(@"Url.AddRoute('{0}', function() {{ return baseUrl.concat('{1}'); }});", name, url);
            }
        }

        private string GetDefaultsAsJson(IDictionary<string, object> defaults)
        {
            StringBuilder defaultsAsJson = new StringBuilder();
            if (defaults.Count > 0)
            {
                defaultsAsJson.Append(", {");

                for (int i = 0; i < defaults.Count; i++)
                {
                    var keyPair = defaults.ElementAt(i);
                    defaultsAsJson.AppendFormat("{0}:'{1}'", keyPair.Key, keyPair.Value);

                    if (!keyPair.Equals(defaults.Last()))
                        defaultsAsJson.Append(",");
                }

                defaultsAsJson.Append("}");
            }
            return defaultsAsJson.ToString();
        }
    }
}