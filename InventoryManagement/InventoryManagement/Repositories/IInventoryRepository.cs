using InventoryManagement.Models;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repositories
{
    public interface IInventoryRepository
    {
        OperationResult AddInventory(Inventory inventory);
        Inventory GetInventoryByProductId(int productId);
        IEnumerable<Inventory> GetAllInventories();
        OperationResult UpdateInventory(int productId, int newQuantity);
    }

}
