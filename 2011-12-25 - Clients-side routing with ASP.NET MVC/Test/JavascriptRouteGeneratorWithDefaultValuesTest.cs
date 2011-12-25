using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace Test
{
    [TestClass]
    public class JavascriptRouteGeneratorWithDefaultValuesTest : JavascriptRouteGeneratorTest
    {
        private Dictionary<string, object> BuildDefaults(object defaults)
        {
            var attr = BindingFlags.Public | BindingFlags.Instance;
            var dict = new Dictionary<string, object>();
            foreach (var property in defaults.GetType().GetProperties(attr))
            {
                if (property.CanRead) 
                {
                    object value = property.GetValue(defaults, null);
                    dict.Add(property.Name, value);
                }
            }
            return dict;
        }

        [TestMethod]
        public void BasicRoute_WithOneDefaultValue()
        {
            var defaults = this.BuildDefaults(new { id = "2" });
            string js = this.generator.Build("Home", "Home/Index/{id}", defaults);
            string expectedJs = @"Url.AddRoute('Home', function(params) {
                                    return baseUrl.concat('Home/Index/', params.id);
                                  }, { id: '2' });";

            JavaScriptAssert.AreEqual(expectedJs, js);
        }

        [TestMethod]
        public void BasicRoute_WithTwoDefaultValue()
        {
            var defaults = this.BuildDefaults(new { controller = "Home", action = "Index" });
            string js = this.generator.Build("Home", "{controller}/{action}", defaults);
            string expectedJs = @"Url.AddRoute('Home', function(params) {
                                    return baseUrl.concat(params.controller, '/', params.action);
                                  }, { controller: 'Home', action: 'Index' });";

            JavaScriptAssert.AreEqual(expectedJs, js);
        }
    }
}
