using InventoryManagement.Context;
using InventoryManagement.Models;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        public OperationResult AddInventory(Inventory inventory)
        {
            var existing = GetInventoryByProductId(inventory.ProductId);
            if (existing != null)
            {
                return new OperationResult { IsSuccess = false, ErrorMessage = "Inventory already exists for this product." };
            }

            InventoryDb.InventoryData.Add(inventory);
            return new OperationResult { IsSuccess = true };
        }

        public Inventory GetInventoryByProductId(int productId)
        {
            return InventoryDb.InventoryData.FirstOrDefault(i => i.ProductId == productId);
        }

        public IEnumerable<Inventory> GetAllInventories()
        {
            return InventoryDb.InventoryData;
        }

        public OperationResult UpdateInventory(int productId, int newQuantity)
        {
            var inventory = GetInventoryByProductId(productId);
            if (inventory == null)
            {
                return new OperationResult { IsSuccess = false, ErrorMessage = "Inventory not found." };
            }

            inventory.QuantityAvailable = newQuantity;
            return new OperationResult { IsSuccess = true };
        }
    }

}
