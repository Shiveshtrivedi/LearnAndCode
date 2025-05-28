using InventoryManagement.Context;
using InventoryManagement.Exceptions;
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
            try
            {
                var existingSupplier = GetSupplierById(supplier.SupplierId);

                SupplierDb.SupplierData.Add(supplier);
                return new OperationResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new OperationResult { IsSuccess = false, ErrorMessage = "Supplier already exists" };
            }

        }

        public OperationResult DeleteSupplier(int supplierId)
        {
           try
            {
                var supplier = GetSupplierById(supplierId);
                SupplierDb.SupplierData.Remove(supplier);
                return new OperationResult { IsSuccess = true };
            }
            catch (Exception ex) 
            {
                return new OperationResult { IsSuccess = false, ErrorMessage = $"Supplier not found {ex.Message}" };
            }
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return SupplierDb.SupplierData; 
        }

        public Supplier GetSupplierById(int supplierId)
        {
            Supplier existingSupplier = SupplierDb.SupplierData.FirstOrDefault(s => s.SupplierId == supplierId);

            if (existingSupplier == null)
                throw new SupplierNotFoundException(supplierId);

            return existingSupplier;
        }
    }
}
