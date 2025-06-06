using InventoryManagement.Models;
using InventoryManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Utils.Interfaces
{
    public interface ICategoryInputHelper
    {
        Category GetCategoryFromUser(ICategoryService categoryService);
    }
}
