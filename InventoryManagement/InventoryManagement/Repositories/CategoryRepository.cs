using InventoryManagement.Context;
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
            var existingCategory = GetCategoryById(category.CategoryId);

            if (existingCategory != null)
            {
                return new OperationResult { IsSuccess = false, ErrorMessage = "Product already exist" };
            }
            CategoryDb.CategoryData.Add(category);

            return new OperationResult { IsSuccess = true };
        }

        public OperationResult DeleteCategory(int categoryId)
        {
            Category existingCategory = GetCategoryById(categoryId);

            if (existingCategory != null) 
            {
                CategoryDb.CategoryData.Remove(existingCategory);

               return new OperationResult { IsSuccess = true };
            }
            else
            {
                return new OperationResult { ErrorMessage = "No Category Exist" };
            }

        }

        public IEnumerable<Category> GetAllCategories()
        {
            return CategoryDb.CategoryData;
        }

        public Category GetCategoryById(int categoryId)
        {
            Category category = CategoryDb.CategoryData.FirstOrDefault(categories => categories.CategoryId == categoryId);

            return category;
        }

        public void UpdateCategory()
        {
            // in-progress
            throw new NotImplementedException();
        }
    }
}
