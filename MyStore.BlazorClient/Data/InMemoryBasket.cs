using MyStore.Models;

namespace MyStore.BlazorClient
{
    public class InMemoryBasket : IBasket
    {
    
        public static List<Product> BasketCatalog { get; set; } = new List<Product>();

        public List<Product> GetProducts()
        {
            return BasketCatalog;
        }
        public void AddProductToBasket(Product product)
        {
            BasketCatalog.Add(product);
        }
        public void DeleteProductFromBasket(Product product)
        {
           BasketCatalog.Remove(product);
        }
        public void ClearAllBasket()
        {
            BasketCatalog.Clear();
        }
    }
    
}
