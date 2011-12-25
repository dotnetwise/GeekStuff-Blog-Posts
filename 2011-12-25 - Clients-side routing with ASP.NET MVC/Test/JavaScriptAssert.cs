using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    public static class JavaScriptAssert
    {
        public static void AreEqual(string expected, string actual)
        {
            string expectedString = expected.ToString().Replace(" ", "").Replace(Environment.NewLine, "");
            string actualString = actual.ToString().Replace(" ", "").Replace(Environment.NewLine, "");
            Assert.AreEqual(expectedString, actualString);
        }
    }
}
