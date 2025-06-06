using InventoryManagementConsole.Services;
using Microsoft.Extensions.Configuration;
using InventoryManagementConsole.DTOs;
using System.Threading.Tasks;
using InventoryManagementConsole.Utils;
using InventoryManagementConsole.Menu.Interfaces;

namespace InventoryManagementConsole.Menus
{
    public class SupplierMenu : IMenu
    {
        public async Task Show()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                                                        .AddJsonFile("appsettings.json")
                                                        .Build();

            IHttpClientFactoryWrapper clientFactoryWrapper = new HttpClientFactory(configuration);
            var service = new SupplierService(clientFactoryWrapper);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Supplier Menu ===");
                Console.WriteLine("1. View all suppliers");
                Console.WriteLine("2. Add supplier");
                Console.WriteLine("3. Delete supplier");
                Console.WriteLine("0. Back");
                Console.Write("Choose option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await service.GetAllSuppliersAsync();
                        break;
                    case "2":
                        var newSupplier = SupplierInputHelper.ReadSupplier();
                        await service.AddSupplierAsync(newSupplier);
                        break;
                    case "3":
                        int id = SupplierInputHelper.ReadSupplierId();
                        await service.DeleteSupplierAsync(id);
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
