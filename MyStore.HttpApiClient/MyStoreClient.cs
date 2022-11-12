using MyStore.Models;
using System.Net.Http.Json;


namespace MyStore.HttpApiClient
{
    public class MyStoreClient : IMyStoreClient
    {
        private const string DefaultHost = $"https://localhost:7116";
        private readonly string _host;
        private readonly HttpClient _httpClient;

        public MyStoreClient(string host = DefaultHost, HttpClient? httpClient = null)
        {
            _host = host;
            _httpClient = httpClient ?? new HttpClient();
        }

        public async Task<IReadOnlyList<Product>> GetProducts()
        {
            var uri = $"{_host}/get_products";
            var response = await _httpClient.GetFromJsonAsync<IReadOnlyList<Product>>(uri);
            return response!;
        }

        public async Task AddProduct(Product product)
        {
            if(product is not null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            var uri = $"{_host}/add_product";
            await _httpClient.PostAsJsonAsync(uri, product);
        }

        public async Task<Product> GetProduct(int id)
        {
            var uri = $"{_host}/get_product";
           
            var product = await _httpClient.GetFromJsonAsync<Product>($"{uri}?productId={id}");
            if(product is null)
            {
                throw new InvalidOperationException("Product can't be null");
            }
            
            return product;
        }

        public async Task DeleteProduct(int id)
        {
            var uri = $"{_host}/delete_product";

            HttpResponseMessage response = await _httpClient.PostAsync($"{uri}?productId={id}", null);
            response.EnsureSuccessStatusCode();

        }

        public async Task UpdateProduct(Product newProduct, int productId)
        {
            var uri = $"{_host}/update_product";
            
            var response = await _httpClient.PutAsJsonAsync($"{uri}?productId={productId}", newProduct);
            response.EnsureSuccessStatusCode();
        }
    }
}