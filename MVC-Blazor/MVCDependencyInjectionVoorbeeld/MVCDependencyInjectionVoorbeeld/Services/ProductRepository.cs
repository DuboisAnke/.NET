using MVCDependencyInjectionVoorbeeld.Entities;
using System.Collections.Generic;

namespace MVCDependencyInjectionVoorbeeld.Services
{
    public class ProductRepository : IProductRepository
    {
        private Dictionary<string, Product> products;

        public ProductRepository()
        {
            Product football = new Product();
            football.Name = "football";
            football.Price = 10m;
            products = new Dictionary<string, Product>();
            products.Add(football.Name, football);
        }

        public IEnumerable<Product> Products => products.Values;
        public Product this[string name] => products[name];

        

        public void Add(Product product)
        {
            products[product.Name] = product;
        }

        public void Delete(Product product)
        {
            products.Remove(product.Name);
        }
    }
}
