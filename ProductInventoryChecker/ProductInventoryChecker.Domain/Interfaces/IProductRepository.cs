using System.Collections.Generic;
using ProductInventoryChecker.Domain;

namespace ProductInventoryChecker.Domain.Interfaces
{
    public interface IProductRepository
    {
        ProductInfo GetById(string id);
        void Add(ProductInfo product);
        void Update(ProductInfo product);
        void Delete(string id);
        IEnumerable<ProductInfo> GetAll();
    }
}
