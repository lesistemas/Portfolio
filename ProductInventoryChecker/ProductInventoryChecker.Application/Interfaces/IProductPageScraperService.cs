using System.Collections.Generic;
using ProductInventoryChecker.Domain;

namespace ProductInventoryChecker.Application.Interfaces
{
    public interface IProductPageScraperService
    {
        List<ProductInfo> ScrapeProductUrls(string baseUrl);
    }
}
