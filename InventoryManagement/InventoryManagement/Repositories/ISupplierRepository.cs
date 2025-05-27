using InventoryManagement.Models;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repositories
{
    public interface ISupplierRepository
    {
        OperationResult AddSupplier(Supplier supplier);
        Supplier GetSupplierById(int supplierId);
        IEnumerable<Supplier> GetAllSuppliers();
        OperationResult DeleteSupplier(int supplierId);
    }
}
