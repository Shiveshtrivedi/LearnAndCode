using InventoryManagement.Models;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMangement.Repositories.Interface
{
    public interface IInventoryRepository
    {
        void AddInventory(Inventory inventory);
        Inventory GetInventoryByProductId(int productId);
        IEnumerable<Inventory> GetAllInventories();
        void UpdateInventory(int productId, int newQuantity);
        void DeleteInventory(int productId);
    }

}
