using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using ProductInventoryChecker.Infrastructure;
using ProductInventoryChecker.Application;
using ProductInventoryChecker.Domain;
using ProductInventoryChecker.Utilities;

namespace ProductInventoryChecker.Application
{
    public class ProductAvailabilityRunner
    {
        private static MessageService _messageService;

        public ProductAvailabilityRunner()
        {
            _messageService = new MessageService(); // Inicializa a instância do MessageService
        }

        public async Task ExecuteTask()
        {
            //Testes de envios Kafka
            await _messageService.SendMessageAsync("DataHora:"+ DateTime.Now.ToString() );

            if ((DateTime.Now.Hour == 22)  && (DateTime.Now.Minute == 0))
            {
                ExecuteProductAvailability();
            }
            
        }

        private async void ExecuteProductAvailability()
        {
            IWebDriver driver = WebDriverFactory.CreateChromeDriver();

            try
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ProductInfoMgMinis2.csv");
                string saleRecordFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SaleRecords.csv");

                var cancellationTokenSource = new CancellationTokenSource();
                var consumerService = new MessageConsumerService();

                Console.CancelKeyPress += (sender, eventArgs) =>
                {
                    eventArgs.Cancel = true;
                    cancellationTokenSource.Cancel();
                };

                Console.WriteLine("Consumidor Kafka iniciado. Pressione Ctrl+C para sair.");
                consumerService.ConsumeMessages(cancellationTokenSource.Token);

                List<SaleRecord> listaItensVendidos;

                var scraper = new ProductPageScraper(driver);
                List<ProductInfo> siteList = scraper.ScrapeProductUrls("https://mgminis.com/loja/");

                var csvList = ListProductManager.LoadCSVToList(filePath);
                siteList = CompareLists.Compare(siteList, csvList, out listaItensVendidos);

                // Envia uma mensagem Kafka com a lista de itens vendidos
                if (listaItensVendidos.Any())
                {
                    string message = $"Itens vendidos: {string.Join(", ", listaItensVendidos.Select(x => x.IdProduct))}";
                    await _messageService.SendMessageAsync(message);
                }

                // Exporta a lista atualizada para o arquivo CSV
                ListProductManager.ExportToCsv(siteList, filePath);
                SaleRecordManager.RecordSale(listaItensVendidos, saleRecordFilePath);

                Console.WriteLine($"Arquivo CSV criado em: {filePath}");
                Console.WriteLine($"Arquivo de vendas criado em: {saleRecordFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
