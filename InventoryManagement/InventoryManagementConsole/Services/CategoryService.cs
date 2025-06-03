using System.Net.Http.Json;
using InventoryManagementConsole.DTOs;

namespace InventoryManagementConsole.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _client;

        public CategoryService(IHttpClientFactoryWrapper clientFactoryWrapper)
        {
            _client = clientFactoryWrapper.GetClient();
        }

        public async Task GetAllCategoriesAsync()
        {
            var categories = await _client.GetFromJsonAsync<List<CategoryDto>>("api/Category/fetchAllCategory");

            Console.WriteLine("=== Categories ===");
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.CategoryId} | {category.CategoryName}");
            }
        }

        public async Task AddCategoryAsync()
        {
            Console.Write("Enter category name: ");
            string name = Console.ReadLine();

            var category = new CategoryDto { CategoryName = name };
            var response = await _client.PostAsJsonAsync("api/Category/addCategory", category);

            Console.WriteLine(response.IsSuccessStatusCode
                ? "Category added successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}");
        }

        public async Task UpdateCategoryAsync()
        {
            Console.Write("Enter category ID to update: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter new name: ");
            string newName = Console.ReadLine();

            var updatedCategory = new CategoryDto
            {
                CategoryId = id,
                CategoryName = newName
            };

            var response = await _client.PutAsJsonAsync($"api/Category/{id}/updateCategory", updatedCategory);

            Console.WriteLine(response.IsSuccessStatusCode
                ? "Category updated successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}");
        }

        public async Task DeleteCategoryAsync()
        {
            Console.Write("Enter category ID to delete: ");
            int id = int.Parse(Console.ReadLine());

            var response = await _client.DeleteAsync($"api/Category/{id}/deleteCategory");

            Console.WriteLine(response.IsSuccessStatusCode
                ? "Category deleted successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}");
        }
    }
}
