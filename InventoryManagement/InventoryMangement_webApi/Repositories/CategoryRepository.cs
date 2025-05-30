using InventoryManagement.Context;
using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagement.Enum;

namespace InventoryManagement.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public OperationResult AddCategory(Category category)
        {
            var existingCategory = CategoryDb.CategoryData.Any(categories => categories.CategoryId == category.CategoryId);

            if (existingCategory)
            {
                return OperationResult.Fail($"Category with ID {category.CategoryId} already exists.", ErrorCode.AlreadyExists);
            }

            CategoryDb.CategoryData.Add(category);
            return OperationResult.Success();
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

            if (category == null)
                throw new CategoryNotFoundException(categoryId);

            return category;
        }
    }
}
