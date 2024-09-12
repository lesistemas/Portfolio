using ProductInventoryChecker.Domain.Interfaces;
using ProductInventoryChecker.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ProductInventoryChecker.Infrastructure.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public ProductInfo GetById(string id)
        {
            return _context.Products.FirstOrDefault(p => p.IdProduct == id);
        }

        public void Add(ProductInfo product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(ProductInfo product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var product = _context.Products.FirstOrDefault(p => p.IdProduct == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ProductInfo> GetAll()
        {
            return _context.Products.ToList();
        }
    }
}
