using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;
using InventoryManagement.Utils;

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
            if (product == null)
                throw new InventoryException("Product cannot be null while adding to inventory.");

            var inventory = new Inventory
            {
                InventoryId = IdGenerator.GetNextId(),
                ProductId = product.ProductId,
                Product = product,
                QuantityAvailable = product.QuantityInStock
            };

            return _inventoryRepository.AddInventory(inventory);
        }

        public OperationResult UpdateInventory(int productId, int newQuantity)
        {
            var result = _inventoryRepository.UpdateInventory(productId, newQuantity);

            var product = _productRepository.GetProductById(productId);
            product.QuantityInStock = newQuantity;
            _productRepository.UpdateProduct(product);

            if (!result.IsSuccess)
                throw new OperationFailedException("UpdateInventory", result.ErrorMessage);

            return result;
        }

        public void DisplayAllInventories()
        {
            throw new NotImplementedException();
        }

      
       

        public IEnumerable<Inventory> GetAllInventories()
        {
            return _inventoryRepository.GetAllInventories();
        }

        public IEnumerable<Product> GetLowStockItems()
        {
            return _productRepository.GetAllProducts()
                    .Where(product => product.QuantityInStock < LowStockThreshold)
                    .ToList();
        }
    }
}
