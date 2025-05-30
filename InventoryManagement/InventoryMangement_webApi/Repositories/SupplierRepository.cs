using InventoryManagement.Context;
using InventoryManagement.Enum;
using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        public OperationResult AddSupplier(Supplier supplier)
        {
            var existingSupplier = SupplierDb.SupplierData.Any(suppliers => suppliers.SupplierId == supplier.SupplierId);

            if (existingSupplier)
            {
                return OperationResult.Fail($"Supplier with ID {supplier.SupplierId} already exists.", ErrorCode.AlreadyExists);
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
                return OperationResult.Fail(ex.Message, ErrorCode.NotFound);
            }
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return SupplierDb.SupplierData;
        }

        public Supplier GetSupplierById(int supplierId)
        {
            var supplier = SupplierDb.SupplierData.FirstOrDefault(suppliers => suppliers.SupplierId == supplierId);

            if (supplier == null)
                throw new SupplierNotFoundException(supplierId);

            return supplier;
        }
    }
}
