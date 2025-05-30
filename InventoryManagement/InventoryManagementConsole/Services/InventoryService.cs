using System.Net.Http.Json;
using InventoryManagementConsole.DTOs;

namespace InventoryManagementConsole.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly HttpClient _client;

        public InventoryService()
        {
            _client = HttpClientFactory.GetClient();
        }

        public async Task RegisterInventoryAsync()
        {
            Console.Write("Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Product Name: ");
            string productName = Console.ReadLine();

            Console.Write("Category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            Console.Write("Supplier ID: ");
            int supplierId = int.Parse(Console.ReadLine());

            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            var product = new ProductDto
            {
                ProductId = productId,
                ProductName = productName,
                CategoryId = categoryId,
                SupplierId = supplierId,
                Price = price,
                QuantityInStock = quantity
            };

            var response = await _client.PostAsJsonAsync("api/Inventory/register", product);
            Console.WriteLine(response.IsSuccessStatusCode
                ? "Inventory registered successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}");
        }

        public async Task UpdateInventoryAsync()
        {
            Console.Write("Enter Product ID to update: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Enter new Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            var response = await _client.PutAsync($"api/Inventory/update?productId={productId}&quantity={quantity}", null);
            Console.WriteLine(response.IsSuccessStatusCode
                ? "Inventory updated successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}");
        }

        public async Task GetAllInventoriesAsync()
        {
            var inventories = await _client.GetFromJsonAsync<List<InventoryDto>>("api/Inventory/all");

            Console.WriteLine("=== All Inventories ===");
            foreach (var inv in inventories)
            {
                Console.WriteLine($"Product ID: {inv.ProductId}, Quantity: {inv.Quantity}");
            }
        }

        public async Task GetLowStockItemsAsync()
        {
            var response = await _client.GetAsync("api/Inventory/lowstock");

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Low Stock Items:");
                Console.WriteLine(body);
            }
            else
            {
                Console.WriteLine($"Failed: {await response.Content.ReadAsStringAsync()}");
            }
        }
    }
}
