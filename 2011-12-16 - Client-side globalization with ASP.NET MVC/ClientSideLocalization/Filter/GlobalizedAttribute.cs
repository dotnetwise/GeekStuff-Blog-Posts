using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;

namespace ClientSideLocalization.Filter
{
    public class GlobalizedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string cultureName = Convert.ToString(filterContext.RouteData.Values["culture"]);
            CultureInfo culture = new CultureInfo(cultureName);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            base.OnActionExecuting(filterContext);
        }
    }
}