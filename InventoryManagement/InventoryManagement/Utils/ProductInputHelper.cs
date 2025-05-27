using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Utils
{
    public static class ProductInputHelper
    {
        public static Product GetInputFromUser(bool isUpdate = false)
        {
            int id;
            if (isUpdate)
            {
                Console.WriteLine("Enter Product Id to update");

                id = int.Parse(Console.ReadLine());
            }
            else
            {
                id = IdGenerator.GetNextId();
            }
                Console.WriteLine("Enter Product Name");
            string productName = Console.ReadLine();
            Console.WriteLine("Enter Product Description");
            string productDescription = Console.ReadLine();
            Console.WriteLine("Enter Quantity");
            int quantityInStock = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Price");
            decimal price = decimal.Parse(Console.ReadLine());

            var product = new Product
            {
                ProductId = id,
                ProductName = productName,
                ProductDescription = productDescription,
                QuantityInStock = quantityInStock,
                Price = price
            };

            return product;
        }
    }
}
