using System.Web.Mvc;
using SubdomainRouting.Models;

namespace SubdomainRouting.Areas.Blog
{
    public class BlogAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Blog"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.Add("Blog_Home", new SubdomainRoute(
                "blog",
                "",
                new { area = this.AreaName, controller = "Home", action = "Index" },
                new string[] { "SubdomainRouting.Areas.Blog.Controllers" }
            ));

            context.Routes.Add("Blog_ShowPost", new SubdomainRoute(
                "blog",
                "posts/{slug}",
                new { area = this.AreaName, controller = "Post", action = "Show" },
                new string[] { "SubdomainRouting.Areas.Blog.Controllers" }
            ));
        }
    }
}
