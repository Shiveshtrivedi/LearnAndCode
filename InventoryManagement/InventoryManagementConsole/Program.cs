using InventoryManagementConsole.Menu.Interfaces;
using InventoryManagementConsole.Menus;

class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Inventory Management ===");
            Console.WriteLine("1. Product Menu");
            Console.WriteLine("2. Category Menu");
            Console.WriteLine("3. Supplier Menu");
            Console.WriteLine("4. Inventory Menu");
            Console.WriteLine("0. Exit");
            Console.Write("Choose option: ");
            string userInput = Console.ReadLine();

            IMenu selectedMenu = userInput switch
            {
                "1" => new ProductMenu(),
                "2" => new CategoryMenu(),
                "3" => new SupplierMenu(),
                "4" => new InventoryMenu(),
                "0" => null,
                _ => null
            };

            if (userInput == "0")
            {
                Console.WriteLine("Exiting the application. Goodbye!");
                return;
            }

            if (selectedMenu != null)
            {
                await selectedMenu.Show();
            }
            else
            {
                Console.WriteLine("Invalid selection. Please try again.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
