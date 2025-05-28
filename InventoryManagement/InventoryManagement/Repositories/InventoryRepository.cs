using InventoryManagement.Context;
using InventoryManagement.Exceptions;
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
        public Inventory GetInventoryByProductId(int productId)
        {
            var existingInventory = InventoryDb.InventoryData.FirstOrDefault(i => i.ProductId == productId);

            if(existingInventory == null)
            {
                throw new InventoryException("Inventory Not Found");
            }

            return existingInventory;
        }

        public OperationResult AddInventory(Inventory inventory)
        {
            try
            {
                var existing = GetInventoryByProductId(inventory.ProductId);

                InventoryDb.InventoryData.Add(inventory);
                return new OperationResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new OperationResult { IsSuccess = false, ErrorMessage = $"Inventory already exists for this product. {ex.Message}" };
            }
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
