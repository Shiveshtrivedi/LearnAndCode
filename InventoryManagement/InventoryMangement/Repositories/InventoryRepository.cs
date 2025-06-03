using InventoryManagement.Context;
using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Utils;
using InventoryManagement.Enum;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        public Inventory GetInventoryByProductId(int productId)
        {
            var existingInventory = InventoryDb.InventoryData.FirstOrDefault(inventories => inventories.ProductId == productId);

            if (existingInventory == null)
            {
                throw new InventoryException("Inventory not found.");
            }

            return existingInventory;
        }

        public void AddInventory(Inventory inventory)
        {
            var existingInventory = InventoryDb.InventoryData.FirstOrDefault(inventories => inventories.ProductId == inventory.ProductId);

            if (existingInventory != null)
            {
                throw new InventoryException($"Inventory for Product ID {inventory.ProductId} already exists.");
            }

            InventoryDb.InventoryData.Add(inventory);
        }

        public IEnumerable<Inventory> GetAllInventories()
        {
            return InventoryDb.InventoryData;
        }

        public void UpdateInventory(int productId, int newQuantity)
        {
            var inventory = InventoryDb.InventoryData.FirstOrDefault(inventories => inventories.ProductId == productId);

            if (inventory == null)
            {
                throw new InventoryException($"Inventory for Product ID {productId} not found.");
            }

            inventory.QuantityAvailable = newQuantity;
        }
    }
}
