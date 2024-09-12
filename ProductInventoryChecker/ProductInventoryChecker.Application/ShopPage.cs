using OpenQA.Selenium;
using System.Collections.Generic;

namespace ProductInventoryChecker.Application
{
    public class ShopPage
    {
        private readonly IWebDriver _driver;

        public ShopPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void NavigateTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public List<string> GetAllProductLinks()
        {
            var productLinks = new List<string>();
            var elements = _driver.FindElements(By.CssSelector("a.woocommerce-LoopProduct-link"));

            foreach (var element in elements)
            {
                productLinks.Add(element.GetAttribute("href"));
            }

            return productLinks;
        }
    }
}
