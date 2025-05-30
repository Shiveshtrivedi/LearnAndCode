using InventoryManagementConsole.Menus;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Inventory Management ===");
            Console.WriteLine("1. Product Menu");
            Console.WriteLine("2. Category Menu");
            Console.WriteLine("3. Supplier Menu");
            Console.WriteLine("0. Exit");
            Console.Write("Choose option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1": ProductMenu.Show(); break;
                case "2": CategoryMenu.Show(); break;
                case "3": SupplierMenu.Show(); break;
                case "0": return;
                default: Console.WriteLine("Invalid option."); break;
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
