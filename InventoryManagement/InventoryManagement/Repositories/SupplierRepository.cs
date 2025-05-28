using InventoryManagement.Context;
using InventoryManagement.Models;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        public OperationResult AddSupplier(Supplier supplier)
        {
            var existing = SupplierDb.SupplierData.FirstOrDefault(s => s.SupplierId == supplier.SupplierId);
            if (existing != null)
                return new OperationResult { IsSuccess = false, ErrorMessage = "Supplier already exists" };

            SupplierDb.SupplierData.Add(supplier);
            return new OperationResult { IsSuccess = true };
        }

        public OperationResult DeleteSupplier(int supplierId)
        {
            var supplier = GetSupplierById(supplierId);
            if (supplier == null)
                return new OperationResult { IsSuccess = false, ErrorMessage = "Supplier not found" };

            SupplierDb.SupplierData.Remove(supplier);
            return new OperationResult { IsSuccess = true };
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return SupplierDb.SupplierData; 
        }

        public Supplier GetSupplierById(int supplierId)
        {
            Supplier existingSupplier = SupplierDb.SupplierData.FirstOrDefault(s => s.SupplierId == supplierId)!;
            return existingSupplier;
        }
    }
}
