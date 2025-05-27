using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Utils
{
    public static class ProductDisplayHelper
    {
        public static void DisplayProduct(Product product)
        {
            Console.WriteLine("Product Details:");
            Console.WriteLine($"ID: {product.ProductId}");
            Console.WriteLine($"Name: {product.ProductName}");
            Console.WriteLine($"Description: {product.ProductDescription}");
            Console.WriteLine($"Quantity: {product.QuantityInStock}");
            Console.WriteLine($"Price: {product.Price:C}");
        }

        public static void DisplayProducts(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                DisplayProduct(product);
            }
        }
    }
}
