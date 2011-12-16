using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using ClientSideLocalization.App_GlobalResources;
using System.Threading;
using System.Text;
using System.Collections;
using ClientSideLocalization.Filter;

namespace ClientSideLocalization.Controllers
{
    [Globalized]
    public class ContentController : Controller
    {
[HttpGet]
public ActionResult LocalizedScript()
{
    string javaScript = SerializeResourceToJavaScript();
    return Content(javaScript, "application/javascript");
}

[NonAction]
private string SerializeResourceToJavaScript()
{
    CultureInfo uiCulture = Thread.CurrentThread.CurrentUICulture;
    var resourceSet = CommonStrings.ResourceManager.GetResourceSet(uiCulture, true, true);

    StringBuilder js = new StringBuilder();
    js.Append("var CommonStrings = {").Append(Environment.NewLine);
    foreach (DictionaryEntry entry in resourceSet)
    {
        js.AppendFormat("\"{0}\": \"{1}\",", entry.Key.ToString(), entry.Value.ToString());
        js.AppendLine();
    }
    js.Append("}");
    return js.ToString();
}
    }
}
