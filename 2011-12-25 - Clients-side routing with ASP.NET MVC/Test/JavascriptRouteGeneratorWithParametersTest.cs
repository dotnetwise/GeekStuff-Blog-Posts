using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class JavascriptRouteGeneratorWithParametersTest : JavascriptRouteGeneratorTest
    {
        [TestMethod]
        public void BasicRoute_WithOneParameter()
        {
            string js = this.generator.Build("Home", "Home/Index/{id}");
            string expectedJs = @"Url.AddRoute('Home', function(params) {
                                    return baseUrl.concat('Home/Index/', params.id);
                                  });";

            JavaScriptAssert.AreEqual(expectedJs, js);
        }

        [TestMethod]
        public void BasicRoute_WithOnlyParameters()
        {
            string js = this.generator.Build("Home", "{controller}");
            string expectedJs = @"Url.AddRoute('Home', function(params) {
                                    return baseUrl.concat(params.controller);
                                  });";

            JavaScriptAssert.AreEqual(expectedJs, js);
        }

        [TestMethod]
        public void AnotherBasicRoute_WithOneParameter()
        {
            string js = this.generator.Build("Admin_Default", "Index/{controller}");
            string expectedJs = @"Url.AddRoute('Admin_Default', function(params) {
                                    return baseUrl.concat('Index/', params.controller);
                                  });";

            JavaScriptAssert.AreEqual(expectedJs, js);
        }

        [TestMethod]
        public void BasicRoute_WithTwoParameters()
        {
            string js = this.generator.Build("Home", "Home/{controller}/{action}");
            string expectedJs = @"Url.AddRoute('Home', function(params) {
                                    return baseUrl.concat('Home/', params.controller, '/', params.action);
                                  });";

            JavaScriptAssert.AreEqual(expectedJs, js);
        }

        [TestMethod]
        public void AnotherBasicRoute_WithTwoParameters()
        {
            string js = this.generator.Build("Home", "{controller}/{action}");
            string expectedJs = @"Url.AddRoute('Home', function(params) {
                                    return baseUrl.concat(params.controller, '/', params.action);
                                  });";

            JavaScriptAssert.AreEqual(expectedJs, js);
        }

        [TestMethod]
        public void AnotherBasicRoute_WithTwoParameters_WithStaticContentBetweenIt()
        {
            string js = this.generator.Build("Home", "Home/{controller}/List/{category}");
            string expectedJs = @"Url.AddRoute('Home', function(params) {
                                    return baseUrl.concat('Home/', params.controller, '/List/', params.category);
                                  });";

            JavaScriptAssert.AreEqual(expectedJs, js);
        }

        [TestMethod]
        public void AnotherBasicRoute_WithOneParametersBetweenStaticContent()
        {
            string js = this.generator.Build("Home", "product/{id}/details");
            string expectedJs = @"Url.AddRoute('Home', function(params) {
                                    return baseUrl.concat('product/', params.id, '/details');
                                  });";

            JavaScriptAssert.AreEqual(expectedJs, js);
        }
    }
}
