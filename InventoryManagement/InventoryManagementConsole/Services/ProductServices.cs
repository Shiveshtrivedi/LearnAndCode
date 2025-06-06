using System.Net.Http.Json;
using System.Text.Json;
using InventoryManagementConsole.DTOs;
using Microsoft.Extensions.Configuration;

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
                    Console.WriteLine($"{product.ProductId} | {product.ProductName} | {product.Price:C} | {product.QuantityInStock}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
            }
            catch (NotSupportedException ex)       
            {
                Console.WriteLine($"Unsupported content type: {ex.Message}");
            }
            catch (JsonException ex)   
            {
                Console.WriteLine($"Invalid JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }


        public async Task GetProductByName(string productName)
        {
            var product = await _client.GetFromJsonAsync<ProductDto>($"api/Product/{productName}/getProductByName");

            Console.WriteLine($"product name {product.ProductName} | {product.Price} | {product.QuantityInStock}");
        }

        public async Task AddProductAsync()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Description: ");
            string description = Console.ReadLine();

            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("CategoryId: ");
            int categoryId = int.Parse(Console.ReadLine());

            Console.Write("SupplierId: ");
            int supplierId = int.Parse(Console.ReadLine());

            var newProduct = new ProductCreateDto
            {
                ProductName = name,
                ProductDescription = description,
                Price = price,
                QuantityInStock = quantity,
                CategoryId = categoryId,
                SupplierId = supplierId
            };

            var response = await _client.PostAsJsonAsync("api/Product/addProduct", newProduct);

            string content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine($"Failed: {content}");
            }
        }

        public async Task AddMultipleProductsAsync()
        {
            var products = new List<ProductCreateDto>();

            Console.Write("Enter how many products to add: ");
            int count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nProduct {i + 1}:");

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Description: ");
                string description = Console.ReadLine();

                Console.Write("Price: ");
                decimal price = decimal.Parse(Console.ReadLine());

                Console.Write("Quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                Console.Write("CategoryId: ");
                int categoryId = int.Parse(Console.ReadLine());

                Console.Write("SupplierId: ");
                int supplierId = int.Parse(Console.ReadLine());

                products.Add(new ProductCreateDto
                {
                    ProductName = name,
                    ProductDescription = description,
                    Price = price,
                    QuantityInStock = quantity,
                    CategoryId = categoryId,
                    SupplierId = supplierId
                });
            }

            var response = await _client.PostAsJsonAsync("api/Product/addMultipleProduct", products);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("All products registered successfully.");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed: {error}");
            }
        }



        public async Task UpdateProductAsync()
        {
            Console.Write("Enter ProductId to update: ");
            int id = int.Parse(Console.ReadLine());

            var product = await _client.GetFromJsonAsync<ProductDto>($"api/Product/{id}/fetchProductById");

            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.Write("New Name: ");
            product.ProductName = Console.ReadLine();

            Console.Write("New Price: ");
            product.Price = decimal.Parse(Console.ReadLine());

            Console.Write("New Qty: ");
            product.QuantityInStock = int.Parse(Console.ReadLine());

            await _client.PutAsJsonAsync("api/Product/updateProduct", new ProductUpdateDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                QuantityInStock = product.QuantityInStock,
                CategoryId = product.CategoryId,
                SupplierId = product.SupplierId
            });

            Console.WriteLine("Updated successfully.");
        }

        public async Task DeleteProductAsync()
        {
            Console.Write("Enter ProductId to delete: ");
            int id = int.Parse(Console.ReadLine());

            var response = await _client.DeleteAsync($"api/Product/{id}/deleteProduct");

            Console.WriteLine(response.IsSuccessStatusCode
                ? "Deleted successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}");
        }
    }
}
