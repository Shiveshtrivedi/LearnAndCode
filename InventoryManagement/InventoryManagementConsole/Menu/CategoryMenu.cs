using InventoryManagementConsole.DTOs;
using InventoryManagementConsole.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using InventoryManagementConsole.Utils;
using InventoryManagementConsole.Menu.Interfaces;

namespace InventoryManagementConsole.Menus
{
    public class CategoryMenu : IMenu
    {
        public async Task Show()
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
                    case "1":
                        try
                        {
                            var categories = await service.GetAllCategoriesAsync();
                            if (categories.Count == 0)
                            {
                                Console.WriteLine("No categories available.");
                            }
                            else
                            {
                                Console.WriteLine("=== Categories ===");
                                foreach (var cat in categories)
                                {
                                    Console.WriteLine($"C.Id : {cat.CategoryId}  | Name :{cat.CategoryName}");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "2":
                        var name = CategoryInputHelper.ReadCategoryName();
                        var category = new CategoryDto { CategoryName = name };
                        var addResult = await service.AddCategoryAsync(category);
                        Console.WriteLine(addResult);
                        break;

                    case "3":
                        var (updateId, newName) = CategoryInputHelper.ReadCategoryUpdateInfo();
                        var updateResult = await service.UpdateCategoryAsync(updateId, newName);
                        Console.WriteLine(updateResult);
                        break;

                    case "4":
                        int deleteId = CategoryInputHelper.ReadCategoryId("delete");
                        var deleteResult = await service.DeleteCategoryAsync(deleteId);
                        Console.WriteLine(deleteResult);
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
