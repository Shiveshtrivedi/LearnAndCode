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
                Console.WriteLine("5. Get Product By Name");
                Console.WriteLine("6. Update Product");
                Console.WriteLine("7. Delete Product");
                Console.WriteLine("8. Check Low Stock Alert");
                Console.WriteLine("9. Update Inventory");
                Console.WriteLine("10. View All Inventory");
                Console.WriteLine("11. Exit");
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
                            var allProduct = _productService.GetAllProducts();
                            foreach (var product in allProduct)
                                ProductDisplayHelper.DisplayProduct(product);
                            break;
                        case "4":
                            Console.Write("Enter Product ID: ");
                            if (int.TryParse(Console.ReadLine(), out int productId))
                            {
                                var product = _productService.GetProductById(productId);
                                if (product != null)
                                    ProductDisplayHelper.DisplayProduct(product);
                                else
                                    Console.WriteLine("Product not found.");
                            }
                            else Console.WriteLine("Invalid ID.");
                            break;
                        case "5":
                            Console.Write("Enter Product Name: ");
                            string productName = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(productName))
                            {
                                var product = _productService.GetProductByName(productName); 
                                if (product != null)
                                    ProductDisplayHelper.DisplayProduct(product);
                                else
                                    Console.WriteLine("Product not found.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Product Name.");
                            }
                            break;

                        case "6":
                            _productService.UpdateProductDetails();
                            break;
                        case "7":
                            Console.Write("Enter Product ID to delete: ");
                            if (int.TryParse(Console.ReadLine(), out int deleteId))
                                _productService.RemoveProduct(deleteId);
                            else Console.WriteLine("Invalid ID.");
                            break;
                        case "8":
                            _inventoryService.AlertIfLowStock();
                            break;
                        case "9":
                            _inventoryService.UpdateInventory();
                            break;
                        case "10":
                            _inventoryService.DisplayAllInventories();
                            break;
                        case "11":
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
