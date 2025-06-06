using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementConsole.Utils
{
    public static class InventoryInputHelper
    {
        public static (int productId, int quantity) ReadUpdateInventoryInfo()
        {
            Console.Write("Enter Product ID to update: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Enter new Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            return (productId, quantity);
        }
    }
}
