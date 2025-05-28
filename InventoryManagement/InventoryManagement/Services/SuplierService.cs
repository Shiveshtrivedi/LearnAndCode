using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Services
{
    public class SuplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SuplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public void AddSupplier()
        {
            Console.WriteLine("Enter Supplier Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Contact Number:");
            string contact = Console.ReadLine();

            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine();

            var supplier = new Supplier
            {
                SupplierId = IdGenerator.GetNextId(),
                SupplierName = name,
                ContactNumber = contact,
            };

            var result = _supplierRepository.AddSupplier(supplier);

            if (result.IsSuccess)
            {
                Console.WriteLine("Supplier added successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to add supplier: {result.ErrorMessage}");
            }
        }

        public void ViewAllSuppliers()
        {
            var suppliers = _supplierRepository.GetAllSuppliers();
            Console.WriteLine("-* Suppliers List *-");
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"ID: {supplier.SupplierId}, Name: {supplier.SupplierName}, Contact: {supplier.ContactNumber}");
            }
        }

        public void DeleteSupplier()
        {
            Console.WriteLine("Enter Supplier ID to delete:");
            if (!int.TryParse(Console.ReadLine(), out int supplierId))
                throw new InventoryException("Invalid supplier ID input.");

            var result = _supplierRepository.DeleteSupplier(supplierId);
            if (!result.IsSuccess)
                throw new OperationFailedException("DeleteSupplier", result.ErrorMessage);

            Console.WriteLine("Supplier deleted successfully.");
        }
    }

}

