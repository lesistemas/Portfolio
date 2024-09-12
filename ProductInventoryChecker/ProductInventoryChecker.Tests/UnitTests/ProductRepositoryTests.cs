using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using ProductInventoryChecker.Infrastructure.Persistence;
using ProductInventoryChecker.Domain;
using System.Linq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ProductInventoryChecker.Tests.UnitTests
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private ProductDbContext _context;
        private ProductRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: "ProductDb")
                .Options;

            _context = new ProductDbContext(options);
            _repository = new ProductRepository(_context);
        }

        [Test]
        public void Add_Product_AddedToDatabase()
        {
            var product = new ProductInfo { IdProduct = "1", Title = "Product A", Quantity = "10" };

            _repository.Add(product);
            var result = _repository.GetById("1");

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
