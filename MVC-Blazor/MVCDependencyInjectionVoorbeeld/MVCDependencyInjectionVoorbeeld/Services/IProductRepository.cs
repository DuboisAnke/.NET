using System.Collections;
using System.Collections.Generic;
using MVCDependencyInjectionVoorbeeld.Entities;

namespace MVCDependencyInjectionVoorbeeld.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        Product this[string name] { get; }
        void Add(Product product);
        void Delete(Product product);
    }
}
