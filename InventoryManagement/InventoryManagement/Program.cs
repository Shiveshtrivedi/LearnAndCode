using InventoryManagement.Models;
using InventoryManagement.Repositories;
using InventoryManagement.Services;
using InventoryManagement.Utils;
using System;

namespace InventoryManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IProductRepository productRepository = new ProductRepository();
            ICategoryRepository categoryRepository = new CategoryRepository();
            ICategoryService categoryService = new CategoryService(categoryRepository);
            IInventoryRepository inventoryRepository = new InventoryRepository();
            IInventoryService inventoryService = new InventoryService(inventoryRepository, productRepository);
            IProductService productService = new ProductService(productRepository, categoryService, inventoryService);

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n==== Product Management Menu ====");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Add Multiple Product");
                Console.WriteLine("3. View All Products");
                Console.WriteLine("4. Get Product By ID");
                Console.WriteLine("5. Update Product");
                Console.WriteLine("6. Delete Product");
                Console.WriteLine("7. Check Low Stock Alert");
                Console.WriteLine("8. Update Inventory");
                Console.WriteLine("9. View All Inventory");
                Console.WriteLine("10. Exit");
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine()!;

                switch (input)
                {
                    case "1":
                        try
                        {
                            productService.AddProduct();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error adding product: {ex.Message}");
                        }
                        break;

                    case "2":
                        try
                        {
                            productService.AddMultipleProduct();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error adding multiple products: {ex.Message}");
                        }
                        break;

                    case "3":
                        try
                        {
                            var allProducts = productService.GetAllProducts();
                            foreach (var product in allProducts)
                            {
                                ProductDisplayHelper.DisplayProduct(product);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error fetching products: {ex.Message}");
                        }
                        break;

                    case "4":
                        try
                        {
                            Console.Write("Enter Product ID: ");
                            int getId = int.Parse(Console.ReadLine()!);
                            var productById = productService.GetProductById(getId);
                            if (productById != null)
                                ProductDisplayHelper.DisplayProduct(productById);
                            else
                                Console.WriteLine("Product not found.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter a numeric Product ID.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error retrieving product: {ex.Message}");
                        }
                        break;

                    case "5":
                        try
                        {
                            productService.UpdateProduct();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error updating product: {ex.Message}");
                        }
                        break;

                    case "6":
                        try
                        {
                            Console.Write("Enter Product ID to delete: ");
                            int deleteId = int.Parse(Console.ReadLine()!);
                            productService.DeleteProduct(deleteId);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter a numeric Product ID.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error deleting product: {ex.Message}");
                        }
                        break;

                    case "7":
                        try
                        {
                            inventoryService.CheckLowStock();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error checking low stock: {ex.Message}");
                        }
                        break;

                    case "8":
                        try
                        {
                            inventoryService.UpdateInventory();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error updating inventory: {ex.Message}");
                        }
                        break;

                    case "9":
                        try
                        {
                            inventoryService.ViewAllInventories();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error viewing inventories: {ex.Message}");
                        }
                        break;

                    case "10":
                        exit = true;
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
