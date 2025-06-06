using InventoryManagement.Context;
using InventoryManagement.Enum;
using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Utils;
using InventoryMangement.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        public void AddSupplier(Supplier supplier)
        {
            var existingSupplier = SupplierDb.SupplierData.Any(suppliers => suppliers.SupplierId == supplier.SupplierId);

            if (existingSupplier)
            {
                throw new DuplicateSupplierException(supplier.SupplierId);

            }

            SupplierDb.SupplierData.Add(supplier);
        }

        public void DeleteSupplier(int supplierId)
        {
            var supplier = GetSupplierById(supplierId);
            SupplierDb.SupplierData.Remove(supplier);    
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return SupplierDb.SupplierData;
        }

        public Supplier GetSupplierById(int supplierId)
        {
            var supplier = SupplierDb.SupplierData.FirstOrDefault(suppliers => suppliers.SupplierId == supplierId);

            return supplier;
        }
    }
}
