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
        private const int LowStockThreshold = 10;
        public InventoryService(IInventoryRepository inventoryRepository, IProductRepository productRepository)
        {
            _inventoryRepository = inventoryRepository;
            _productRepository = productRepository;
        }
        public OperationResult AddInventory(Product product)
        {
            if(product == null)
            {
                return new OperationResult { ErrorMessage = "Product not Found"};
            }

            var inventory = new Inventory
            {
                InventoryId = IdGenerator.GetNextId(),
                ProductId = product.ProductId,
                Product = product,
                QuantityAvailable = product.QuantityInStock,
            };

            return _inventoryRepository.AddInventory(inventory);
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

        public void CheckLowStock()
        {
            IEnumerable<Product> products = _productRepository.GetAllProducts();

            IEnumerable<Product> lowStockItems = products.Where(product => product.QuantityInStock < LowStockThreshold).ToList();

            if(lowStockItems.Any())
            {
                Console.WriteLine("Low Stock Alert !!!");
                foreach(Product item in lowStockItems)
                {
                    Console.WriteLine($"Product ID : {item.ProductId}, Name : {item.ProductName}");
                }
            }
            else
            {
                Console.WriteLine("All product have sufficient stock");
            }


        }
    }
}
