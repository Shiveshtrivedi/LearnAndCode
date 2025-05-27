using InventoryManagement.Models;
using InventoryManagement.Repositories;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IProductRepository _productRepository;

        public InventoryService(IInventoryRepository inventoryRepository, IProductRepository productRepository)
        {
            _inventoryRepository = inventoryRepository;
            _productRepository = productRepository;
        }
        public void AddInventory()
        {
            Console.WriteLine("Enter Product Id:");
            int productId = int.Parse(Console.ReadLine());

            var product = _productRepository.GetProductById(productId);
            if (product == null)
            {
                Console.WriteLine("Product does not exist.");
                return;
            }

            Console.WriteLine("Enter Quantity:");
            int quantity = int.Parse(Console.ReadLine());

            var inventory = new Inventory
            {
                InventoryId = IdGenerator.GetNextId(),
                ProductId = productId,
                Product = product,
                QuantityAvailable = quantity
            };

            var result = _inventoryRepository.AddInventory(inventory);
            Console.WriteLine(result.IsSuccess ? "Inventory added successfully." : $"Error: {result.ErrorMessage}");
        }

        public void UpdateInventory()
        {
            Console.WriteLine("Enter Product Id:");
            int productId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter New Quantity:");
            int newQty = int.Parse(Console.ReadLine());

            var result = _inventoryRepository.UpdateInventory(productId, newQty);
            Console.WriteLine(result.IsSuccess ? "Inventory updated." : $"Error: {result.ErrorMessage}");
        }

        public void ViewAllInventories()
        {
            var inventories = _inventoryRepository.GetAllInventories();
            foreach (var inv in inventories)
            {
                Console.WriteLine($"InventoryId: {inv.InventoryId}, Product: {inv.Product?.ProductName}, Quantity: {inv.QuantityAvailable}");
            }
        }
    }
}
