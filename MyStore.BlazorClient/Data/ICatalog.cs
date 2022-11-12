using System.Collections.Concurrent;
using MyStore.Models;

namespace MyStore.BlazorClient
{
    public interface ICatalog
    {
        void AddProduct(Product product);
        ConcurrentBag<Product> GetProducts();
        void DeleteProduct(int id);
        
    }
}