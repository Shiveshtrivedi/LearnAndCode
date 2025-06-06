using InventoryManagementConsole.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementConsole.Utils
{
    public static class SupplierInputHelper
    {
        public static SupplierDto ReadSupplier()
        {
            Console.Write("Enter Supplier Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Contact Number: ");
            string contact = Console.ReadLine();

            return new SupplierDto
            {
                SupplierName = name,
                ContactNumber = contact,
            };
        }

        public static int ReadSupplierId()
        {
            Console.Write("Enter Supplier ID to delete: ");
            return int.Parse(Console.ReadLine());
        }
    }
}
