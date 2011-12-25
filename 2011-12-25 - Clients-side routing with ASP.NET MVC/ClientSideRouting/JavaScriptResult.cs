using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace ClientSideRouting
{
    public class JavaScriptResult : ContentResult
    {
        public JavaScriptResult(StringBuilder js)
            : this(js.ToString())
        {
        }

        public JavaScriptResult(string js)
        {
            this.Content = js;
            this.ContentType = "text/javascript";
        }
    }
}