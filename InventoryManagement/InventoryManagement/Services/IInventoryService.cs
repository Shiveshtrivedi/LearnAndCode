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
        OperationResult AddInventory(Product product);
        void ViewAllInventories();
        void UpdateInventory();
        void CheckLowStock();
    }

}
