using InventoryManagementConsole.Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using InventoryManagementConsole.Utils;
using InventoryManagementConsole.Menu.Interfaces;

namespace InventoryManagementConsole.Menus
{
    public class InventoryMenu : IMenu
    {
        public async Task Show() 
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
                Console.WriteLine("1. Update Inventory Quantity");
                Console.WriteLine("2. View All Inventories");
                Console.WriteLine("3. View Low Stock Items");
                Console.WriteLine("0. Back");

                Console.Write("Select option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        var (productId, quantity) = InventoryInputHelper.ReadUpdateInventoryInfo();
                        await service.UpdateInventoryAsync(productId, quantity);
                        break;
                    case "2":
                        await service.GetAllInventoriesAsync();
                        break;
                    case "3":
                        await service.GetLowStockItemsAsync();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
