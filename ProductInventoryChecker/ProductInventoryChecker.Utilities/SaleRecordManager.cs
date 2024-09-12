using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using ProductInventoryChecker.Domain;

namespace ProductInventoryChecker.Utilities
{
 

    public static class SaleRecordManager
    {
        public static void RecordSale(List<SaleRecord> products, string filePath)
        {
            

            try
            {
                var records = new List<SaleRecord>();
                if (File.Exists(filePath))
                {
                    using var reader = new StreamReader(filePath);
                    var csvConfig1 = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.CurrentCulture)
                    {
                        Delimiter = ";",
                    };
                    using var csv1 = new CsvReader(reader, csvConfig1);
                    records = csv1.GetRecords<SaleRecord>().ToList();
                }

                records.AddRange(products);
                

                using var writer = new StreamWriter(filePath, append: true);
                var csvConfig = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.CurrentCulture)
                {
                    Delimiter = ";",
                };
                using var csv = new CsvWriter(writer, csvConfig);
                csv.WriteRecords(records);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Erro ao registrar a venda: {ex.Message}");
            }
        }
    }
}
