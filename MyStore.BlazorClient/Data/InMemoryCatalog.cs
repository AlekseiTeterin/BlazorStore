using System.Collections.Concurrent;
using MyStore.Models;

namespace MyStore.BlazorClient
{
    public class InMemoryCatalog : ICatalog
    {
        private readonly object _syncObj = new();
        public int productsCount;
        private ConcurrentBag<Product> Products { get; set; } = new()
        {
            new Product(1, "Чистый код", 100),
            new Product(2, "Чистая архитектура", 200),
            new Product(3, "Entity framework в действии", 100)     
        };

        
        public void AddProduct(Product product)
        {
            lock (_syncObj)
            {
                Products.Add(product);
                
            }
        }

        public ConcurrentBag<Product> GetProducts()
        {
            lock (_syncObj)
            {                
                return Products;
            }
        }

        public void DeleteProduct(int id)
        {
            var list = Products.ToList();
            list.RemoveAt(id);
            Products = new ConcurrentBag<Product>(list);
        }

        
    }
}
