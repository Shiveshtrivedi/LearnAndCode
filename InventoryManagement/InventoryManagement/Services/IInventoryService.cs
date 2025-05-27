using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Services
{
    public interface IInventoryService
    {
        void AddInventory();
        void ViewAllInventories();
        void UpdateInventory();
    }

}
