using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using ProductInventoryChecker.Application.Interfaces;
using ProductInventoryChecker.Domain;

namespace ProductInventoryChecker.Application
{
    public class ProductPageScraper : IProductPageScraperService
    {
        private readonly IWebDriver _driver;

        public ProductPageScraper(IWebDriver driver)
        {
            _driver = driver;
        }

        public List<ProductInfo> ScrapeProductUrls(string baseUrl)
        {
            List<ProductInfo> products = new List<ProductInfo>();
            _driver.Navigate().GoToUrl(baseUrl);
            int page = 1;

            while (true)
            {
                System.Threading.Thread.Sleep(3000); // Espera 3 segundos

                IList<IWebElement> productElements = _driver.FindElements(By.CssSelector("a.woocommerce-LoopProduct-link"));

                bool foundBuyButton = false;

                foreach (IWebElement productElement in productElements)
                {
                    string productUrl = productElement.GetAttribute("href");
                    _driver.Navigate().GoToUrl(productUrl);

                    System.Threading.Thread.Sleep(2000); // Espera 2 segundos

                    try
                    {
                        IWebElement buyButton = _driver.FindElement(By.CssSelector("button.single_add_to_cart_button"));
                        if (buyButton != null)
                        {
                            string productId = buyButton.GetAttribute("value");
                            string titleName = _driver.FindElement(By.CssSelector("h1.product_title")).Text;
                            string price = _driver.FindElement(By.CssSelector("p.price")).Text;
                            string quantity = GetMaxQuantity();

                            products.Add(new ProductInfo
                            {
                                Url = productUrl,
                                IdProduct = productId,
                                Title = titleName,
                                Price = price,
                                Quantity = quantity,
                                MaxQuantity = quantity,
                                Date = DateTime.Now
                            });

                            Console.WriteLine($"URL: {productUrl}");
                            Console.WriteLine($"ID: {productId}");
                            Console.WriteLine($"Title: {titleName}");
                            Console.WriteLine($"Price: {price}");
                            Console.WriteLine($"Quantity: {quantity}");
                            Console.WriteLine();

                            foundBuyButton = true;
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        foundBuyButton = false;
                    }

                    _driver.Navigate().Back();
                }

                if (!foundBuyButton)
                {
                    break;
                }

                try
                {
                    IWebElement nextPageButton = _driver.FindElement(By.CssSelector("a.next.page-numbers"));
                    nextPageButton.Click();
                    page++;

                    if (page >= 6) break;
                }
                catch (NoSuchElementException)
                {
                    break;
                }
            }

            return products;
        }

        private string GetMaxQuantity()
        {
            try
            {
                var inputQtd = _driver.FindElement(By.ClassName("input-text"));
                return inputQtd.GetAttribute("max");
            }
            catch (NoSuchElementException)
            {
                return "N/A";
            }
        }
    }
}
