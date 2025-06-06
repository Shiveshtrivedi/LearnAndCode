using InventoryManagement.Context;
using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagement.Enum;
using InventoryMangement.Exceptions;

namespace InventoryManagement.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public void AddCategory(Category category)
        {
            var existingCategory = CategoryDb.CategoryData.Any(categories => categories.CategoryId == category.CategoryId);

            if (existingCategory)
            {
                throw new DuplicateCategoryException(category.CategoryId);
            }

            CategoryDb.CategoryData.Add(category);
        }

        public OperationResult DeleteCategory(int categoryId)
        {
            try
            {
                var existingCategory = GetCategoryById(categoryId);
                CategoryDb.CategoryData.Remove(existingCategory);

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message, ErrorCode.NotFound);
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return CategoryDb.CategoryData;
        }

        public Category GetCategoryById(int categoryId)
        {
            var category = CategoryDb.CategoryData.FirstOrDefault(categories => categories.CategoryId == categoryId);

            return category;
        }
    }
}
