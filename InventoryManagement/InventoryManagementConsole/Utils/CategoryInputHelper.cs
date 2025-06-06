using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementConsole.Utils
{
    public static class CategoryInputHelper
    {
        public static string ReadCategoryName()
        {
            Console.Write("Enter category name: ");
            return Console.ReadLine();
        }

        public static int ReadCategoryId(string action)
        {
            Console.Write($"Enter category ID to {action}: ");
            return int.Parse(Console.ReadLine());
        }

        public static (int id, string newName) ReadCategoryUpdateInfo()
        {
            Console.Write("Enter category ID to update: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter new name: ");
            string newName = Console.ReadLine();

            return (id, newName);
        }
    }
}
