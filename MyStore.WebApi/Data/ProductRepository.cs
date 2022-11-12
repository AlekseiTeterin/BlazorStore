using Microsoft.EntityFrameworkCore;
using MyStore.Models;


namespace MyStore.WebApi.Data
{
    public class ProductRepository : IProductsRepository
    {
        private readonly AppDbContext _dbContext;
        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProduct(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(int productId)
        {
            var prod = await _dbContext.Products.FirstAsync(it => it.Id == productId);
            _dbContext.Products.Remove(prod);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<Product> GetProductById(int productId) => 
            await _dbContext.Products.FirstAsync(it => it.Id == productId);
       

        public async Task UpdateProduct(Product product, int productId)
        {
            var prod = await _dbContext.Products.FirstAsync(it => it.Id == productId);
            
            prod.Name = product.Name;
            prod.Price = product.Price;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Product>> GetProducts() => 
            await _dbContext.Products.ToListAsync();

    }
}
