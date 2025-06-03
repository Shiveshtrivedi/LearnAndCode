using System.Net.Http.Json;
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
            var products = await _client.GetFromJsonAsync<List<ProductDto>>("api/Product/fetchAllProduct");

            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductId} | {product.ProductName} | {product.Price:C}");
            }
        }

        public async Task AddProductAsync()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Description: ");
            string description = Console.ReadLine();

            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Qty: ");
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

            Console.WriteLine(response.IsSuccessStatusCode
                ? "Product added successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}");
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
