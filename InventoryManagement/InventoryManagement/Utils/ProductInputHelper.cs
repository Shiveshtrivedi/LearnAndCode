using InventoryManagement.Context;
using InventoryManagement.Models;
using InventoryManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Utils
{
    public static class ProductInputHelper
    {
        public static Product GetInputFromUser(ICategoryService categoryService, ISupplierService supplierService, bool isUpdate = false)
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

            Category category = CategoryInputHelper.GetCategoryFromUser(categoryService);

            Supplier supplierObj;

            Console.WriteLine("Press (1) for Select an existing supplier or (2) Add new supplier? ");
            string supplierChoice = Console.ReadLine();

            if (supplierChoice == "1")
            {
                Console.WriteLine("Enter Supplier Id:");
                if (int.TryParse(Console.ReadLine(), out int supplierId))
                {
                    supplierObj = SupplierDb.SupplierData.FirstOrDefault(s => s.SupplierId == supplierId);
                    if (supplierObj == null)
                    {
                        Console.WriteLine("Invalid supplier, assigning default.");
                        supplierObj = new Supplier { SupplierId = 0, SupplierName = "Unknown" };
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, assigning default supplier.");
                    supplierObj = new Supplier { SupplierId = 0, SupplierName = "Unknown" };
                }
            }
            else
            {
                supplierObj = SupplierInputHelper.GetInputFromUser();
            }


            var product = new Product
            {
                ProductId = id,
                ProductName = productName,
                ProductDescription = productDescription,
                QuantityInStock = quantityInStock,
                Price = price,
                CategoryId = category.CategoryId,
                Category = category,
                SupplierId = supplierObj.SupplierId,
                Supplier = supplierObj
            };

            return product;
        }
    }
}
