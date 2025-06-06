using InventoryManagement.Models;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Services
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        void DeleteCategory(int categoryId);
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
        void UpdateCategory(Category updatedCategory);
        Category GetOrCreateUncategorizedCategory(int categoryId);

    }
}
