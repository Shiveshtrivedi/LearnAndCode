using System;

namespace InventoryManagement.Exceptions
{
    public class DuplicateSupplierException : Exception
    {
        public int SupplierId { get; }

        public DuplicateSupplierException(int supplierId)
            : base($"Supplier with ID {supplierId} already exists.")
        {
            SupplierId = supplierId;
        }
    }
}
