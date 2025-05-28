using InventoryManagement.Context;
using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public OperationResult AddCategory(Category category)
        {
            try 
            {
                var existingCategory = GetCategoryById(category.CategoryId);

                CategoryDb.CategoryData.Add(category);

                return new OperationResult { IsSuccess = true };
            }
            catch 
            {
                return new OperationResult { IsSuccess = false, ErrorMessage = "Product already exist" };
            }
        }

        public OperationResult DeleteCategory(int categoryId)
        {
            try
            {
                Category existingCategory = GetCategoryById(categoryId);
                CategoryDb.CategoryData.Remove(existingCategory);
                
                return new OperationResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new OperationResult { ErrorMessage = $"No Category Exist {ex.Message}" };
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return CategoryDb.CategoryData;
        }

        public Category GetCategoryById(int categoryId)
        {
            Category category = CategoryDb.CategoryData.FirstOrDefault(categories => categories.CategoryId == categoryId);

            if (category == null)
                throw new CategoryNotFoundException(categoryId);

            return category;
        }

        public void UpdateCategory()
        {
            // in-progress
            throw new NotImplementedException();
        }
    }
}
