using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public OperationResult AddCategory(Category category)
        {
            var result = _categoryRepository.AddCategory(category);

            return result;
        }

        public void DeleteCategory(int categoryId)
        {
            var result = _categoryRepository.DeleteCategory(categoryId);

            if (result.IsSuccess)
            {
                throw new OperationFailedException("Delete Category", result.ErrorMessage);
            }

            Console.WriteLine("Category deleted successfully.");

        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        public Category GetCategoryById(int categoryId)
        {
            Category category = _categoryRepository.GetCategoryById(categoryId);

            if (category == null)
            {
                throw new CategoryNotFoundException(categoryId);
            }

            return category;
        }

        public void UpdateCategory(Category updatedCategory)
        {
            var existingCategory = _categoryRepository.GetCategoryById(updatedCategory.CategoryId);
            if (existingCategory == null)
            {
                throw new CategoryNotFoundException(updatedCategory.CategoryId);
            }

            existingCategory.CategoryName = updatedCategory.CategoryName;

            Console.WriteLine("Category updated successfully.");
        }
    }
}
