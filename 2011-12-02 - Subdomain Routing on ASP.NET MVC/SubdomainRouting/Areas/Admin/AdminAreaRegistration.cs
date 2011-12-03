using System.Web.Mvc;
using SubdomainRouting.Models;

namespace SubdomainRouting.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Admin"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.Add("Admin_Home", new SubdomainRoute(
                "admin",
                "",
                new { area = this.AreaName, controller = "Home", action = "Index" },
                new string[] { "SubdomainRouting.Areas.Admin.Controllers" }
            ));

            context.Routes.Add("Admin_Login", new SubdomainRoute(
                "admin",
                "login",
                new { area = this.AreaName, controller = "Login", action = "Index" },
                new string[] { "SubdomainRouting.Areas.Admin.Controllers" }
            ));
        }
    }
}
