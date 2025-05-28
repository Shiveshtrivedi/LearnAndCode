using InventoryManagement.Models;
using InventoryManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Utils
{
    public static class CategoryInputHelper
    {
        public static Category GetCategoryFromUser(ICategoryService categoryService)
        {
            var categories = categoryService.GetAllCategories().ToList();

            Console.WriteLine("Available Categories:");
            foreach (var cat in categories)
            {
                Console.WriteLine($"{cat.CategoryId}: {cat.CategoryName}");
            }

            Console.WriteLine("Enter existing Category ID or type 'new' to create a new category:");
            string input = Console.ReadLine();

            if (input.Trim().ToLower() == "new")
            {
                Console.WriteLine("Enter New Category Name:");
                string newName = Console.ReadLine();

                var newCategory = new Category
                {
                    CategoryId = IdGenerator.GetNextId(),
                    CategoryName = newName
                };

                var result = categoryService.AddCategory(newCategory);

                if (result.IsSuccess)
                {
                    return newCategory;
                }

                Console.WriteLine($"Error adding category: {result.ErrorMessage}");
                return new Category { CategoryId = 0, CategoryName = "Uncategorized" };
            }

            if (int.TryParse(input, out int categoryId))
            {
                var category = categoryService.GetCategoryById(categoryId);
                if (category != null)
                {
                    return category;
                }

                Console.WriteLine("Invalid category ID. Defaulting to 'Uncategorized'");
            }

            return new Category { CategoryId = 0, CategoryName = "Uncategorized" };
        }
    }
}
