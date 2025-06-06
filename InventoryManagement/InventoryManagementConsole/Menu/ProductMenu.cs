using InventoryManagementConsole.Services;
using Microsoft.Extensions.Configuration;
using InventoryManagementConsole.DTOs;
using System.Threading.Tasks;
using InventoryManagementConsole.Utils;
using InventoryManagementConsole.Menu.Interfaces;

namespace InventoryManagementConsole.Menus
{
    public class ProductMenu : IMenu
    {
        public async Task Show()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                                                        .AddJsonFile("appsettings.json")
                                                        .Build();

            IHttpClientFactoryWrapper clientFactoryWrapper = new HttpClientFactory(configuration);
            var service = new ProductService(clientFactoryWrapper);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Product Menu ===");
                Console.WriteLine("1. View all products");
                Console.WriteLine("2. Add product");
                Console.WriteLine("3. Add Multiple product");
                Console.WriteLine("4. Update product");
                Console.WriteLine("5. Delete product");
                Console.WriteLine("0. Back");
                Console.Write("Choose option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await service.GetAllProductsAsync();
                        break;
                    case "2":
                        var newProduct = ProductInputHelper.ReadProductCreateDto();
                        await service.AddProductAsync(newProduct);
                        break;
                    case "3":
                        var multipleProducts = ProductInputHelper.ReadMultipleProductCreateDtos();
                        await service.AddMultipleProductsAsync(multipleProducts);
                        break;
                    case "4":
                        var updatedProduct = await ProductInputHelper.ReadProductUpdateDtoAsync(service);
                        if (updatedProduct != null)
                        {
                            await service.UpdateProductAsync(updatedProduct);
                        }
                        break;
                    case "5":
                        int deleteId = ProductInputHelper.ReadProductId("delete");
                        await service.DeleteProductAsync(deleteId);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
