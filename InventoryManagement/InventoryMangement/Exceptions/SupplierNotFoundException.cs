using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Exceptions
{
    public class SupplierNotFoundException : InventoryException
    {
        public SupplierNotFoundException(int supplierId)
            : base($"Supplier with ID {supplierId} not found.") { }
    }
}
