using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ProductInventoryChecker.Application;
using System.Collections.Generic;

namespace ProductInventoryChecker.Tests.UnitTests
{
    [TestFixture]
    public class ProductAvailabilityCheckerTests
    {
        private IWebDriver _driver;
        private ProductAvailabilityChecker _checker;

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _checker = new ProductAvailabilityChecker(_driver);
        }

        [Test]
        public void CheckAllProductQuantities_ValidUrl_PrintsProductAvailability()
        {
            // Suponha que o URL seja um URL válido
            _checker.CheckAllProductQuantities("https://example.com");

            // Verificações podem ser baseadas na saída de console ou em outras validações
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
