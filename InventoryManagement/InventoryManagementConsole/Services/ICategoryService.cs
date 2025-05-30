using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementConsole.Services
{
    public interface ICategoryService
    {
        Task GetAllCategoriesAsync();
        Task AddCategoryAsync();
        Task UpdateCategoryAsync();
        Task DeleteCategoryAsync();
    }
}
