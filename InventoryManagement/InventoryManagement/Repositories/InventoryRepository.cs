using InventoryManagement.Context;
using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Enum;

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
            Inventory existingInventory = InventoryDb.InventoryData.FirstOrDefault(inventories => inventories.ProductId == inventory.ProductId);

            if (existingInventory != null)
            {
                return OperationResult.Fail("Supplier already exists.", ErrorCode.AlreadyExists);
            }

            InventoryDb.InventoryData.Add(inventory);
            return OperationResult.Success();
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
                return OperationResult.Fail("Supplier already exists.", ErrorCode.AlreadyExists);
            }

            inventory.QuantityAvailable = newQuantity;
            return OperationResult.Success();
        }
    }

}
