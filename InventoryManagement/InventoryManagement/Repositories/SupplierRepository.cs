using InventoryManagement.Context;
using InventoryManagement.Enum;
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
            var existingSupplier = SupplierDb.SupplierData.Any(suppliers => suppliers.SupplierId == supplier.SupplierId);

            if (existingSupplier)
            {
                return OperationResult.Fail("Supplier already exists.", ErrorCode.AlreadyExists);
            }

            SupplierDb.SupplierData.Add(supplier);

            return OperationResult.Success();
        }

        public OperationResult DeleteSupplier(int supplierId)
        {
           try
            {
                var supplier = GetSupplierById(supplierId);
                SupplierDb.SupplierData.Remove(supplier);
                return OperationResult.Success();
            }
            catch (Exception ex) 
            {
                return OperationResult.Fail("Supplier already exists.", ErrorCode.AlreadyExists);
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
