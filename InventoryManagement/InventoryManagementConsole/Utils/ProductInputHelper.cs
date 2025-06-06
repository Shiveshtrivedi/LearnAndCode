using InventoryManagementConsole.DTOs;
using InventoryManagementConsole.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementConsole.Utils
{
    public static class ProductInputHelper
    {
        public static ProductCreateDto ReadProductCreateDto()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Description: ");
            string description = Console.ReadLine();

            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("CategoryId: ");
            int categoryId = int.Parse(Console.ReadLine());

            Console.Write("SupplierId: ");
            int supplierId = int.Parse(Console.ReadLine());

            return new ProductCreateDto
            {
                ProductName = name,
                ProductDescription = description,
                Price = price,
                QuantityInStock = quantity,
                CategoryId = categoryId,
                SupplierId = supplierId
            };
        }

        public static List<ProductCreateDto> ReadMultipleProductCreateDtos()
        {
            var products = new List<ProductCreateDto>();

            Console.Write("Enter how many products to add: ");
            int count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nProduct {i + 1}:");
                products.Add(ReadProductCreateDto());
            }

            return products;
        }

        public static async Task<ProductUpdateDto?> ReadProductUpdateDtoAsync(Services.IProductService service)
        {
            Console.Write("Enter ProductId to update: ");
            int id = int.Parse(Console.ReadLine());

            var product = await service.GetProductByIdAsync(id);

            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return null;
            }

            Console.Write("New Name: ");
            product.ProductName = Console.ReadLine();

            Console.Write("New Price: ");
            product.Price = decimal.Parse(Console.ReadLine());

            Console.Write("New Qty: ");
            product.QuantityInStock = int.Parse(Console.ReadLine());

            return new ProductUpdateDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                QuantityInStock = product.QuantityInStock,
                CategoryId = product.CategoryId,
                SupplierId = product.SupplierId
            };
        }

        public static int ReadProductId(string action)
        {
            Console.Write($"Enter ProductId to {action}: ");
            return int.Parse(Console.ReadLine());
        }
    }

}
