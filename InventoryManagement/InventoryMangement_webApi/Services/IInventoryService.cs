using InventoryManagement.Models;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Services
{
    public interface IInventoryService
    {
        OperationResult RegisterInventory(Product product);
        void DisplayAllInventories();
        IEnumerable<Inventory> GetAllInventories();
        OperationResult UpdateInventory(int productId, int newQuantity);
        IEnumerable<Product> GetLowStockItems();
    }

}
