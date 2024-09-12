using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using ProductInventoryChecker.Application.Interfaces;
using ProductInventoryChecker.Domain;

namespace ProductInventoryChecker.Application
{
    public class ProductAvailabilityChecker : IProductAvailabilityService
    {
        private readonly IWebDriver _driver;

        public ProductAvailabilityChecker(IWebDriver driver)
        {
            _driver = driver;
        }

        public void CheckAllProductQuantities(string shopUrl)
        {
            var shopPage = new ShopPage(_driver);
            var productPage = new ProductPageScraper(_driver);

            shopPage.NavigateTo(shopUrl);

            var productLinks = shopPage.GetAllProductLinks();

            foreach (var link in productLinks)
            {
                _driver.Navigate().GoToUrl(link);

                string productName = string.Empty;//productPage.ScrapeProductUrls();
                string availability = string.Empty;//productPage.GetAvailability();

                Console.WriteLine($"Produto: {productName} - Disponibilidade: {availability}");
            }
        }
    }
}
