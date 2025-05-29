using InventoryManagement.Context;
using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Enum;

namespace InventoryManagement.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public OperationResult AddCategory(Category category)
        {
            var exisitngCategory = CategoryDb.CategoryData.Any(categories => categories.CategoryId == category.CategoryId);

            if(exisitngCategory)
            {
                return OperationResult.Fail("Supplier already exists.", ErrorCode.AlreadyExists);
            }

            CategoryDb.CategoryData.Add(category);
            return OperationResult.Success();
        }

        public OperationResult DeleteCategory(int categoryId)
        {
            try
            {
                Category existingCategory = GetCategoryById(categoryId);
                CategoryDb.CategoryData.Remove(existingCategory);

                return OperationResult.Success();           
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"No Category Exist ${ex.Message}", ErrorCode.NotFound);
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
