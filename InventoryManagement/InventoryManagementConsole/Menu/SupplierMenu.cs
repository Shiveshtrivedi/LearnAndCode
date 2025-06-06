using InventoryManagementConsole.Services;
using Microsoft.Extensions.Configuration;

namespace InventoryManagementConsole.Menus
{
    public class SupplierMenu
    {
        public static async Task Show()
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
                    case "1": await service.GetAllSuppliersAsync(); break;
                    case "2": await service.AddSupplierAsync(); break;
                    case "3": await service.DeleteSupplierAsync(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option."); break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
