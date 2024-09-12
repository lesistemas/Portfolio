using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductInventoryChecker.Domain
{
    public static class CompareLists
    {
        public static List<ProductInfo> Compare(List<ProductInfo> siteList, List<ProductInfo> csvList,
            out List<SaleRecord> listaItensVendidos)
        {
            listaItensVendidos = new List<SaleRecord>();

            foreach (var csvItem in csvList)
            {
                var siteItem = siteList.FirstOrDefault(x => x.IdProduct == csvItem.IdProduct);

                // localizado
                if (siteItem != null)
                {
                    int qtdVenda = string.IsNullOrEmpty(csvItem.Quantity) ? 0 : (int.Parse(csvItem.Quantity) - int.Parse(siteItem.Quantity));

                    for (int i = 0; i < qtdVenda; i++)
                    {
                        listaItensVendidos.Add(new SaleRecord() { IdProduct = csvItem.IdProduct, Timestamp = DateTime.Now });
                    }

                    // Atualiza 'MaxQuantity' e 'Date'
                    siteItem.MaxQuantity = csvItem.MaxQuantity;
                    siteItem.Date = csvItem.Date;
                }
                else
                {
                    int qtdVenda = int.Parse(csvItem.Quantity);

                    for (int i = 0; i < qtdVenda; i++)
                    {
                        listaItensVendidos.Add(new SaleRecord() { IdProduct = csvItem.IdProduct, Timestamp = DateTime.Now });
                    }

                    // Adiciona o item que não existe na listaSite
                    siteList.Add(new ProductInfo
                    {
                        IdProduct = csvItem.IdProduct,
                        Url = csvItem.Url,
                        Title = csvItem.Title,
                        Price = csvItem.Price,
                        MaxQuantity = csvItem.MaxQuantity,
                        Quantity = "0",
                        Date = csvItem.Date
                    });
                }
            }

            return siteList;
        }
    }
}
