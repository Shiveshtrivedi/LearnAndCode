using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Tests.MockData
{
    public static class ProductMockData
    {
        public static List<Product> GetMockProducts() => new List<Product>
        {
            new Product { ProductId = 1, ProductName = "Laptop", ProductDescription = "Gaming Laptop", QuantityInStock = 10, Price = 999 },
            new Product { ProductId = 2, ProductName = "Mouse", ProductDescription = "Wireless Mouse", QuantityInStock = 50, Price = 25 }
        };

        public static Product GetNewProduct() =>
            new Product { ProductId = 3, ProductName = "Keyboard", ProductDescription = "Mechanical", QuantityInStock = 20, Price = 45 };
    }
}
