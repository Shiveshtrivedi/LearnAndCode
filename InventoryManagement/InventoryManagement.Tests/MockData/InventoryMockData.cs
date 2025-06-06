using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Tests.MockData
{
    public static class InventoryRepositoryMockData
    {
        public static List<Inventory> Inventories => new()
        {
            new Inventory { ProductId = 1, QuantityAvailable = 100 },
            new Inventory { ProductId = 2, QuantityAvailable = 50 }
        };
    }
}
