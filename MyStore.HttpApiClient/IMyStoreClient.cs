
using MyStore.Models;

namespace MyStore.HttpApiClient
{
    public interface IMyStoreClient
    {
        Task AddProduct(Product product);
        Task<IReadOnlyList<Product>> GetProducts();

        Task<Product> GetProduct(int id);

        Task DeleteProduct(int id);

        Task UpdateProduct(Product newProduct, int productId);
    }
}