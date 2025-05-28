using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Utils
{
    public static class SupplierInputHelper
    {
        public static Supplier GetInputFromUser(bool isUpdate = false, Supplier existingSupplier = null)
        {
            var supplier = new Supplier();

            Console.WriteLine("Enter Supplier Name:");
            string nameInput = Console.ReadLine()!;
            supplier.SupplierName = string.IsNullOrWhiteSpace(nameInput)
                ? existingSupplier?.SupplierName ?? "Unnamed Supplier"
                : nameInput;

            Console.WriteLine("Enter Contact Number:");
            string contactInput = Console.ReadLine()!;
            supplier.ContactNumber = string.IsNullOrWhiteSpace(contactInput)
                ? existingSupplier?.ContactNumber ?? "N/A"
                : contactInput;

            return supplier;
        }
    }
}
