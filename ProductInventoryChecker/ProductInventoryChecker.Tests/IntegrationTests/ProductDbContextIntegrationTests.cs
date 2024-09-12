using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using ProductInventoryChecker.Infrastructure.Persistence;
using ProductInventoryChecker.Domain;
using System.Linq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ProductInventoryChecker.Tests.IntegrationTests
{
    [TestFixture]
    public class ProductDbContextIntegrationTests
    {
        private ProductDbContext _context;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: "ProductDb")
                .Options;

            _context = new ProductDbContext(options);
        }

        [Test]
        public void Add_Product_AddedToDatabase()
        {
            var product = new ProductInfo { IdProduct = "1", Title = "Product A", Quantity = "10" };
            _context.Products.Add(product);
            _context.SaveChanges();

            var result = _context.Products.FirstOrDefault(p => p.IdProduct == "1");

            Assert.IsNotNull(result);
            Assert.AreEqual("Product A", result.Title);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}
