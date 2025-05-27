using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Services
{
    public interface ISupplierService
    {
        void AddSupplier();
        void ViewAllSuppliers();
        void DeleteSupplier();
    }
}
