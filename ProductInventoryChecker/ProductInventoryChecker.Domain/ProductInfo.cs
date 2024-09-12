using System;

namespace ProductInventoryChecker.Domain
{
    public class ProductInfo
    {
        public string Url { get; set; }
        public string IdProduct { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string MaxQuantity { get; set; }
        public string Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
