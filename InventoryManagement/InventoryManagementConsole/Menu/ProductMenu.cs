using InventoryManagementConsole.Services;

namespace InventoryManagementConsole.Menus
{
    public class ProductMenu
    {
        public static async void Show()
        {
            var service = new ProductService();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Product Menu ===");
                Console.WriteLine("1. View all products");
                Console.WriteLine("2. Add product");
                Console.WriteLine("3. Update product");
                Console.WriteLine("4. Delete product");
                Console.WriteLine("0. Back");
                Console.Write("Choose option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1": await service.GetAllProductsAsync(); break;
                    case "2": await service.AddProductAsync(); break;
                    case "3": await service.UpdateProductAsync(); break;
                    case "4": await service.DeleteProductAsync(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option."); break;
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
