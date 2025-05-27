using InventoryManagement.Models;
using InventoryManagement.Repositories;
using InventoryManagement.Services;
using InventoryManagement.Utils;

namespace InventoryManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IProductRepository productRepository = new ProductRepository();
            ICategoryRepository categoryRepository = new CategoryRepository();
            ICategoryService categoryService = new CategoryService(categoryRepository);
            IProductService productService = new ProductService(productRepository,categoryService);

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n==== Product Management Menu ====");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. View All Products");
                Console.WriteLine("3. Get Product By ID");
                Console.WriteLine("4. Update Product");
                Console.WriteLine("5. Delete Product");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        productService.AddProduct();
                        break;

                    case "2":
                        var allProducts = productService.GetAllProducts();
                        foreach (var product in allProducts)
                        {
                            ProductDisplayHelper.DisplayProduct(product);
                        }
                        break;

                    case "3":
                        Console.Write("Enter Product ID: ");
                        int getId = int.Parse(Console.ReadLine());
                        var productById = productService.GetProductById(getId);
                        if (productById != null)
                            ProductDisplayHelper.DisplayProduct(productById);
                        else
                            Console.WriteLine("Product not found.");
                        break;

                    case "4":
                        productService.UpdateProduct();
                        break;

                    case "5":
                        Console.Write("Enter Product ID to delete: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        productService.DeleteProduct(deleteId);
                        break;

                    case "6":
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
