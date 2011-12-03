using System.Web.Mvc;
using SubdomainRouting.Models;

namespace SubdomainRouting.Areas.Site
{
    public class SiteAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Site"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.Add("Site_Home", new SubdomainRoute(
                "",
                "",
                new { area = this.AreaName, controller = "Home", action = "Index" },
                new string[] { "SubdomainRouting.Areas.Site.Controllers" }
            ));
        }
    }
}
