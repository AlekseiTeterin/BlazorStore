using MyStore.Models;

namespace MyStore.WebApi.Data
{
    public interface IProductsRepository
    {
        Task<Product> GetProductById(int Id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product, int Id);

        Task DeleteProduct(int Id);

        Task<IReadOnlyList<Product>> GetProducts();
    }
}
