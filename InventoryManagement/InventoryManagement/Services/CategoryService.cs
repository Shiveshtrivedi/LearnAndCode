using InventoryManagement.Models;
using InventoryManagement.Repositories;
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

        public void AddCategory(Category category)
        {
            var result = _categoryRepository.AddCategory(category);

            if (result.IsSuccess)
            {
                Console.WriteLine("Category added successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to add category: {result.ErrorMessage}");
            }
        }

        public void DeleteCategory(int categoryId)
        {
            var result = _categoryRepository.DeleteCategory(categoryId);

            if (result.IsSuccess)
            {
                Console.WriteLine("Category deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to delete category: {result.ErrorMessage}");
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _categoryRepository.GetCategoryById(categoryId);
        }

        public void UpdateCategory(Category updatedCategory)
        {
            var existingCategory = _categoryRepository.GetCategoryById(updatedCategory.CategoryId);
            if (existingCategory == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            existingCategory.CategoryName = updatedCategory.CategoryName;

            Console.WriteLine("Category updated successfully.");
        }
    }
}
