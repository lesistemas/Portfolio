using Microsoft.EntityFrameworkCore.Infrastructure;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ProductInventoryChecker.Application;
using System.Collections.Generic;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ProductInventoryChecker.Tests.IntegrationTests
{
    [TestFixture]
    public class ProductPageScraperIntegrationTests
    {
        private IWebDriver _driver;
        private ProductPageScraper _scraper;

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _scraper = new ProductPageScraper(_driver);
        }

        [Test]
        public void ScrapeProductUrls_ValidUrl_ReturnsProductList()
        {
            var products = _scraper.ScrapeProductUrls("https://example.com");

            Assert.IsNotNull(products);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
