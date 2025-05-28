using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Utils.Interfaces
{
    public interface ISupplierInputHelper
    {
        Supplier GetInputFromUser(bool isUpdate = false, Supplier existingSupplier = null);
    }
}
