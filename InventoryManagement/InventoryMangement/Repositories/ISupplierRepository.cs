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
        void AddSupplier(Supplier supplier);
        Supplier GetSupplierById(int supplierId);
        IEnumerable<Supplier> GetAllSuppliers();
        void DeleteSupplier(int supplierId);
    }
}
