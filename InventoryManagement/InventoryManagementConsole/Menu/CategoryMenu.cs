using InventoryManagementConsole.Services;
using Microsoft.Extensions.Configuration;

namespace InventoryManagementConsole.Menus
{
    public class CategoryMenu
    {
        public static async void Show()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                                                        .AddJsonFile("appsettings.json")
                                                        .Build();

            IHttpClientFactoryWrapper clientFactoryWrapper = new HttpClientFactory(configuration);
            var service = new CategoryService(clientFactoryWrapper);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Category Menu ===");
                Console.WriteLine("1. View all categories");
                Console.WriteLine("2. Add category");
                Console.WriteLine("3. Update category");
                Console.WriteLine("4. Delete category");
                Console.WriteLine("0. Back");
                Console.Write("Choose option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1": await service.GetAllCategoriesAsync(); break;
                    case "2": await service.AddCategoryAsync(); break;
                    case "3": await service.UpdateCategoryAsync(); break;
                    case "4": await service.DeleteCategoryAsync(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option."); break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
