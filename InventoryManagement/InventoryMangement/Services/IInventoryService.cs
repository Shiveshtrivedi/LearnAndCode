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
        void RegisterInventory(Product product);
        IEnumerable<Inventory> GetAllInventories();
        void UpdateInventory(int productId, int newQuantity);
        IEnumerable<Product> GetLowStockItems();
    }

}
