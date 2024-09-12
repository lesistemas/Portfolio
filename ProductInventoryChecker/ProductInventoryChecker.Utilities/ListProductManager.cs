using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
using CsvHelper;
using ProductInventoryChecker.Domain;

namespace ProductInventoryChecker.Utilities
{
    public static class ListProductManager
    {
        public static List<ProductInfo> LoadCSVToList(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<ProductInfo>();

            using var reader = new StreamReader(filePath);
            var csvConfig = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.CurrentCulture)
            {
                Delimiter = ";", // Define o delimitador como ponto e vírgula
            };
            using var csv = new CsvReader(reader, csvConfig);

            // Lê o conteúdo do CSV para uma lista de objetos
            return csv.GetRecords<ProductInfo>().ToList();
        }

        public static void ExportToCsv(List<ProductInfo> products, string filePath)
        {
            using var writer = new StreamWriter(filePath);
            var csvConfig = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.CurrentCulture)
            {
                Delimiter = ";", // Define o delimitador como ponto e vírgula
            };
            using var csv = new CsvWriter(writer, csvConfig);
            csv.WriteRecords(products);
        }
    }
}
