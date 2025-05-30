using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Tests.MockData
{
    public static class SupplierRepositoryMockData
    {
        public static List<Supplier> Suppliers => new()
        {
            new Supplier { SupplierId = 1, SupplierName = "Supplier One", ContactNumber = "1234567890" },
            new Supplier { SupplierId = 2, SupplierName = "Supplier Two", ContactNumber = "0987654321" }
        };
    }
}
