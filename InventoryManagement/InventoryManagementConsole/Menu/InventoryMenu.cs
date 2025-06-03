using InventoryManagementConsole.Services;
using Microsoft.Extensions.Configuration;

namespace InventoryManagementConsole.Menus
{
    public class InventoryMenu
    {
        public static async void Show()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                                                         .AddJsonFile("appsettings.json")
                                                         .Build(); 

            IHttpClientFactoryWrapper clientFactoryWrapper = new HttpClientFactory(configuration);
            var service = new InventoryService(clientFactoryWrapper);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Inventory Menu ===");
                Console.WriteLine("1. Register Inventory");
                Console.WriteLine("2. Update Inventory Quantity");
                Console.WriteLine("3. View All Inventories");
                Console.WriteLine("4. View Low Stock Items");
                Console.WriteLine("0. Back");

                Console.Write("Select option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1": await service.RegisterInventoryAsync(); break;
                    case "2": await service.UpdateInventoryAsync(); break;
                    case "3": await service.GetAllInventoriesAsync(); break;
                    case "4": await service.GetLowStockItemsAsync(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option."); break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
