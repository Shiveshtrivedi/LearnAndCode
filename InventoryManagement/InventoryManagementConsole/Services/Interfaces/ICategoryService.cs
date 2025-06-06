using InventoryManagementConsole.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementConsole.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task<string> AddCategoryAsync(CategoryDto category);
        Task<string> UpdateCategoryAsync(int id, string newName);
        Task<string> DeleteCategoryAsync(int id);
    }
}
