using System.Net.Http.Json;
using System.Text.Json;
using InventoryManagementConsole.DTOs;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace InventoryManagementConsole.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;

        public ProductService(IHttpClientFactoryWrapper httpClientFactoryWrapper)
        {
            _client = httpClientFactoryWrapper.GetClient();
        }

        public async Task GetAllProductsAsync()
        {
            try
            {
                var response = await _client.GetAsync("api/Product/fetchAllProduct");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode} - {errorContent}");
                    return;
                }

                var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();

                if (products == null || products.Count == 0)
                {
                    Console.WriteLine("No products available.");
                    return;
                }

                
                foreach (var product in products)
                {
                    Console.WriteLine($"P.ID : {product.ProductId} | Name : {product.ProductName} | Price : {product.Price:C} | Quantity : {product.QuantityInStock}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        public async Task<ProductDto?> GetProductByIdAsync(int productId)
        {
            try
            {
                var product = await _client.GetFromJsonAsync<ProductDto>($"api/Product/{productId}/fetchProductById");
                return product;
            }
            catch 
            {
                return null;
            }
        }

        public async Task AddProductAsync(ProductCreateDto newProduct)
        {
            var response = await _client.PostAsJsonAsync("api/Product/addProduct", newProduct);

            string content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(response.IsSuccessStatusCode
                ? content
                : $"Failed: {content}");
        }

        public async Task AddMultipleProductsAsync(List<ProductCreateDto> products)
        {
            var response = await _client.PostAsJsonAsync("api/Product/addMultipleProduct", products);

            Console.WriteLine(response.IsSuccessStatusCode
                ? "All products registered successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}");
        }

        public async Task UpdateProductAsync(ProductUpdateDto updatedProduct)
        {
            await _client.PutAsJsonAsync("api/Product/updateProduct", updatedProduct);
            Console.WriteLine("Updated successfully.");
        }

        public async Task DeleteProductAsync(int productId)
        {
            var response = await _client.DeleteAsync($"api/Product/{productId}/deleteProduct");

            Console.WriteLine(response.IsSuccessStatusCode
                ? "Deleted successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}");
        }
    }
}
