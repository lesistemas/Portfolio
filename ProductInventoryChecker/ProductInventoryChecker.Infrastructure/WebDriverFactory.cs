using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ProductInventoryChecker.Infrastructure
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized"); // Inicia o navegador maximizado
            options.AddArgument("--disable-extensions"); // Desativa extensões do navegador
            options.AddArgument("--disable-gpu"); // Desativa GPU
            options.AddArgument("--no-sandbox"); // Desativa sandbox para ambiente Docker, por exemplo

            return new ChromeDriver(options);
        }
    }
}
