using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SubdomainRouting.Areas.Blog.Controllers
{
    public class PostController : Controller
    {
        public ActionResult Show(string slug)
        {
            return View("Show", (object)slug);
        }
    }
}
