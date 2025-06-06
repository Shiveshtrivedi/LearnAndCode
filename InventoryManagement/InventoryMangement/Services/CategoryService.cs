using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;
using InventoryManagement.Utils;

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
            category.CategoryId = IdGenerator.GetNextId();
            _categoryRepository.AddCategory(category);
        }

        public void DeleteCategory(int categoryId)
        {
            var result = _categoryRepository.DeleteCategory(categoryId);

            if (!result.IsSuccess)
            {
                throw new OperationFailedException("Delete Category", result.ErrorMessage);
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        public Category GetCategoryById(int categoryId)
        {
            var category = _categoryRepository.GetCategoryById(categoryId);

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
        }

        public Category GetOrCreateUncategorizedCategory(int categoryId)
        {
            var category = _categoryRepository.GetCategoryById(categoryId);

            if (category == null)
            {
                category = _categoryRepository
                    .GetAllCategories()
                    .FirstOrDefault(c => c.CategoryName.Equals("Uncategorized", StringComparison.OrdinalIgnoreCase));

                if (category == null)
                {
                    category = new Category
                    {
                        CategoryId = IdGenerator.GetNextId(),
                        CategoryName = "Uncategorized"
                    };

                    _categoryRepository.AddCategory(category);
                }
            }

            return category;
        }

    }
}
