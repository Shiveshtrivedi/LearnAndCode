using InventoryManagement.Context;
using InventoryManagement.Enum;
using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;
using InventoryManagement.Tests.MockData;
using System;
using System.Linq;
using Xunit;

namespace InventoryManagement.Tests
{
    public class CategoryRepositoryTests : IDisposable
    {
        private readonly CategoryRepository _repository;

        public CategoryRepositoryTests()
        {
            CategoryDb.CategoryData.Clear();
            CategoryDb.CategoryData.AddRange(CategoryRepositoryMockData.Categories);

            _repository = new CategoryRepository();
        }

        public void Dispose()
        {
            CategoryDb.CategoryData.Clear();
        }

        [Fact]
        public void GetAllCategories_ReturnsAllCategories()
        {
            var categories = _repository.GetAllCategories();

            Assert.Equal(2, categories.Count());
        }

        [Fact]
        public void GetCategoryById_ExistingId_ReturnsCategory()
        {
            var category = _repository.GetCategoryById(1);

            Assert.Equal("Electronics", category.CategoryName);
        }

        [Fact]
        public void GetCategoryById_NonExistingId_ThrowsCategoryNotFoundException()
        {
            Assert.Throws<CategoryNotFoundException>(() => _repository.GetCategoryById(99));
        }

        [Fact]
        public void AddCategory_NewCategory_ReturnsSuccess()
        {
            var newCategory = new Category { CategoryId = 3, CategoryName = "Books" };

            var result = _repository.AddCategory(newCategory);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void AddCategory_NewCategory_AddsCategoryToDb()
        {
            var newCategory = new Category { CategoryId = 3, CategoryName = "Books" };
            _repository.AddCategory(newCategory);

            Assert.Contains(newCategory, CategoryDb.CategoryData);
        }

        [Fact]
        public void AddCategory_ExistingCategoryId_ReturnsFail()
        {
            var duplicateCategory = new Category { CategoryId = 1, CategoryName = "Duplicate" };

            var result = _repository.AddCategory(duplicateCategory);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void AddCategory_ExistingCategoryId_ReturnsAlreadyExistsErrorCode()
        {
            var duplicateCategory = new Category { CategoryId = 1, CategoryName = "Duplicate" };

            var result = _repository.AddCategory(duplicateCategory);

            Assert.Equal(ErrorCode.AlreadyExists, result.ErrorCode);
        }

        [Fact]
        public void DeleteCategory_ExistingId_ReturnsSuccess()
        {
            var result = _repository.DeleteCategory(1);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void DeleteCategory_ExistingId_RemovesCategoryFromDb()
        {
            _repository.DeleteCategory(1);

            Assert.DoesNotContain(CategoryDb.CategoryData, c => c.CategoryId == 1);
        }

        [Fact]
        public void DeleteCategory_NonExistingId_ReturnsFail()
        {
            var result = _repository.DeleteCategory(99);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void DeleteCategory_NonExistingId_ReturnsNotFoundErrorCode()
        {
            var result = _repository.DeleteCategory(99);

            Assert.Equal(ErrorCode.NotFound, result.ErrorCode);
        }
    }
}
