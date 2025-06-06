using InventoryManagement.Exceptions;
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
        public OperationResult RegisterInventory(Product product)
        {
            if(product == null)
            {
                throw new InventoryException("Product cannot be null while adding into inventory.");
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

        public void UpdateInventory(Product product = null)
        {
            int productId; 
            int newQuantity;
            if (product == null)
            {
                Console.WriteLine("Enter Product Id:");
                if (!int.TryParse(Console.ReadLine(), out productId))
                    throw new InventoryException("Invalid Product ID input.");

                Console.WriteLine("Enter New Quantity:");
                if (!int.TryParse(Console.ReadLine(), out newQuantity))
                    throw new InventoryException("Invalid quantity input.");

            }

            else
            {
                productId = product.ProductId;
                newQuantity = product.QuantityInStock;
            }

            var result = _inventoryRepository.UpdateInventory(productId, newQuantity);
            var fetchProduct = _productRepository.GetProductById(productId);
            fetchProduct.QuantityInStock = newQuantity;
            var updateProduct = _productRepository.UpdateProduct(fetchProduct);

            if (!result.IsSuccess)
                throw new OperationFailedException("UpdateInventory", result.ErrorMessage);

            Console.WriteLine(result.IsSuccess ? "Inventory updated." : $"Error: {result.ErrorMessage}");
        }

        public void DisplayAllInventories()
        {
            var inventories = _inventoryRepository.GetAllInventories();
            foreach (var inv in inventories)
            {
                Console.WriteLine($"InventoryId: {inv.InventoryId}, Product: {inv.Product?.ProductName}, Quantity: {inv.QuantityAvailable}");
            }
        }

        public void AlertIfLowStock()
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
