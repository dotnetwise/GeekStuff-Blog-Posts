using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NamedStringFormat;
using System.Threading;
using System.Globalization;

namespace Test
{
    [TestClass]
    public class FormatInjectTest
    {
        private Ticket ticket;

        [TestInitialize]
        public void SetUp()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            Company company = new Company("Star Wars", 120242124.52);
            Contact owner = new Contact("Luke Skywalker", company);
            this.ticket = new Ticket("123456", owner);
        }

        [TestMethod]
        public void NoFormat()
        {
            string actual = "My name is".Inject(null);
            string expected = "My name is";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SingleParameterFormat()
        {
            string actual = "My name is {Name}".Inject(new { Name = "Guilherme" });
            string expected = "My name is Guilherme";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DoubleParameterFormat()
        {
            string actual = "My name is {Name} and I live in {Country}".Inject(new { Name = "Guilherme", Country = "Brazil" });
            string expected = "My name is Guilherme and I live in Brazil";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SinglePaddingFormat()
        {
            string actual = "{Country,10}".Inject(new { Country = "Brazil" });
            string expected = "    Brazil";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CurrencyFormat()
        {
            string actual = "{Value:C2}".Inject(new { Value = 124.10 });
            string expected = "$124.10";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TwoAdvancedParametersFormat()
        {
            string actual = "{Country,10}{Value:C1}".Inject(new { Country = "Brazil", Value = 224.12 });
            string expected = "    Brazil$224.1";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DateFormat()
        {
            string actual = "{Date:MMddyyyy}".Inject(new { Date = new DateTime(2012, 1, 6) });
            string expected = "01062012";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NumberFormat()
        {
            string actual = "{Number:#,##0}".Inject(new { Number = 1000000000 });
            string expected = "1,000,000,000";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MixPaddingAndCurrencyFormat()
        {
            string actual = "{Value,10:C2}".Inject(new { Value = 531.35 });
            string expected = "   $531.35";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AnotherCultureCurrencyFormat()
        {
            string actual = "{Value:C2}".Inject(new { Value = 12.15 }, new CultureInfo("pt-BR"));
            string expected = "R$ 12,15";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ClassSimplePropertyFormat()
        {
            string actual = "{Number}".Inject(ticket);
            string expected = "123456";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ClassComplexPropertyFormat()
        {
            string actual = "{Owner.Name}".Inject(ticket);
            string expected = "Luke Skywalker";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ClassComplexPropertyWithPaddingFormat()
        {
            string actual = "{Owner.Name,20}".Inject(ticket);
            string expected = "      Luke Skywalker";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ClassComplexNestedPropertyFormat()
        {
            string actual = "{Owner.Company.Name}".Inject(ticket);
            string expected = "Star Wars";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ClassWithTwoComplexNestedPropertyFormat()
        {
            string actual = "{Owner.Company.Name} - {Owner.Company.Revenue:C2}".Inject(ticket);
            string expected = "Star Wars - $120,242,124.52";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenPropertyIsNotFound_ItShouldNotThrowException()
        {
            string actual = "{CreatedOn}".Inject(ticket);
            string expected = "{CreatedOn}";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WhenNestedPropertyIsNotFound_ItShouldNotThrowException()
        {
            string actual = "{Owner.FullName}".Inject(ticket);
            string expected = "{Owner.FullName}";

            Assert.AreEqual(expected, actual);
        }
    }
}
