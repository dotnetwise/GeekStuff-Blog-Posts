using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientSideLocalization.App_GlobalResources;
using ClientSideLocalization.Filter;

namespace ClientSideLocalization.Controllers
{
    [Globalized]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {            
            return View();
        }
    }
}
