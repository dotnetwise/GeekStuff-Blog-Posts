using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientSideRouting;

namespace Test
{
    [TestClass]
    public class JavascriptRouteGeneratorTest
    {
        protected JavascriptRouteGenerator generator;

        [TestInitialize]
        public void SetUp()
        {
            this.generator = new JavascriptRouteGenerator();
        }

        [TestMethod]
        public void BasicRoute()
        {
            string js = this.generator.Build("Home", "Home/Index");
            string expectedJs = @"Url.AddRoute('Home', function() {
                                    return baseUrl.concat('Home/Index');
                                  });";

            JavaScriptAssert.AreEqual(expectedJs, js);
        }
    }
}
