using NUnit.Framework;
using ProductInventoryChecker.Domain;
using System.Collections.Generic;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ProductInventoryChecker.Tests.UnitTests
{
    [TestFixture]
    public class CompareListsTests
    {
        [Test]
        public void Compare_ValidLists_UpdatesAndAddsProducts()
        {
            var siteList = new List<ProductInfo>
            {
                new ProductInfo { IdProduct = "1", Title = "Product A", Quantity = "10" },
                new ProductInfo { IdProduct = "2", Title = "Product B", Quantity = "20" }
            };

            var csvList = new List<ProductInfo>
            {
                new ProductInfo { IdProduct = "1", Title = "Product A", Quantity = "15" },
                new ProductInfo { IdProduct = "3", Title = "Product C", Quantity = "30" }
            };

            var updatedList = CompareLists.Compare(siteList, csvList);

            Assert.AreEqual(3, updatedList.Count);
            Assert.IsTrue(updatedList.Exists(p => p.IdProduct == "1" && p.Quantity == "15"));
            Assert.IsTrue(updatedList.Exists(p => p.IdProduct == "3"));
        }
    }
}
