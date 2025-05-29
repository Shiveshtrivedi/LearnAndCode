using InventoryManagement.Services;
using InventoryManagement.Utils;

namespace InventoryManagement.Setup
{
    public class MenuRouter
    {
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;

        public MenuRouter(IProductService productService, IInventoryService inventoryService)
        {
            _productService = productService;
            _inventoryService = inventoryService;
        }

        public void ShowMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n+++ Product Management Menu +++");
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
                Console.WriteLine();

                try
                {
                    switch (input)
                    {
                        case "1":
                            _productService.RegisterProduct();
                            break;
                        case "2":
                            _productService.RegisterMultipleProduct();
                            break;
                        case "3":
                            var all = _productService.GetAllProducts();
                            foreach (var p in all)
                                ProductDisplayHelper.DisplayProduct(p);
                            break;
                        case "4":
                            Console.Write("Enter Product ID: ");
                            if (int.TryParse(Console.ReadLine(), out int id))
                            {
                                var prod = _productService.GetProductById(id);
                                if (prod != null)
                                    ProductDisplayHelper.DisplayProduct(prod);
                                else
                                    Console.WriteLine("Product not found.");
                            }
                            else Console.WriteLine("Invalid ID.");
                            break;
                        case "5":
                            _productService.UpdateProductDetails();
                            break;
                        case "6":
                            Console.Write("Enter Product ID to delete: ");
                            if (int.TryParse(Console.ReadLine(), out int delId))
                                _productService.RemoveProduct(delId);
                            else Console.WriteLine("Invalid ID.");
                            break;
                        case "7":
                            _inventoryService.AlertIfLowStock();
                            break;
                        case "8":
                            _inventoryService.UpdateInventory();
                            break;
                        case "9":
                            _inventoryService.DisplayAllInventories();
                            break;
                        case "10":
                            Console.WriteLine("Exiting...");
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Something went wrong: {ex.Message}");
                
                }
            }
        }
    }
}
