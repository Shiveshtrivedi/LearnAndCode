using InventoryManagement.Models;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMangement.Services.Interface
{
    public interface ISupplierService
    {
        IEnumerable<Supplier> GetAllSuppliers();
        Supplier GetSupplierById(int supplierId);
        void AddSupplier(Supplier supplier);
        void DeleteSupplier(int supplierId);
        Supplier GetOrCreateDefaultSupplier(int supplierId);
    }
}
