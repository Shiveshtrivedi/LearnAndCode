using InventoryManagement.Models;
using InventoryManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Utils.Interfaces
{
    public interface IProductInputHelper
    {
        Product GetInputFromUser(ICategoryService categoryService, ISupplierService supplierService, bool isUpdate = false);
    }
}
