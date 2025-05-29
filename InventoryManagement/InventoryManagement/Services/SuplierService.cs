using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;
using InventoryManagement.Utils;
using InventoryManagement.Utils.Interfaces;
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
        private readonly ISupplierInputHelper _supplierInputHelper;

        public SuplierService(ISupplierRepository supplierRepository, ISupplierInputHelper supplierInputHelper)
        {
            _supplierRepository = supplierRepository;
            _supplierInputHelper = supplierInputHelper;
        }

        public void AddSupplier()
        {
            Supplier supplierInput = _supplierInputHelper.GetInputFromUser();

            var supplier = new Supplier
            {
                SupplierId = IdGenerator.GetNextId(),
                SupplierName = supplierInput.SupplierName,
                ContactNumber = supplierInput.ContactNumber,
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
                if(!suppliers.Any())
                {
                    Console.WriteLine($"ID: {supplier.SupplierId}, Name: {supplier.SupplierName}, Contact: {supplier.ContactNumber}");
                }
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

