using System.Net.Http.Json;
using InventoryManagementConsole.DTOs;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace InventoryManagementConsole.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly HttpClient _client;

        public InventoryService(IHttpClientFactoryWrapper clientFactoryWrapper)
        {
            _client = clientFactoryWrapper.GetClient();
        }

        public async Task UpdateInventoryAsync(int productId, int quantity)
        {
            var response = await _client.PutAsync($"api/Inventory/updateInventory?productId={productId}&quantity={quantity}", null);
            Console.WriteLine(response.IsSuccessStatusCode
                ? "Inventory updated successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}");
        }

        public async Task GetAllInventoriesAsync()
        {
            var inventories = await _client.GetFromJsonAsync<List<InventoryDto>>("api/Inventory/fetchAllInvetory");

            Console.WriteLine("=== All Inventories ===");
            foreach (var inventory in inventories)
            {
                Console.WriteLine($"Product ID: {inventory.ProductId}, Quantity: {inventory.QuantityAvailable}");
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
