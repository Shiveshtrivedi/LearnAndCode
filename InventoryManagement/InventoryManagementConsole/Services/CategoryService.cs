using System.Net.Http.Json;
using System.Text.Json;
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

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var response = await _client.GetAsync("api/Category/fetchAllCategory");

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");

            var categories = await response.Content.ReadFromJsonAsync<List<CategoryDto>>();

            return categories ?? new List<CategoryDto>();
        }

        public async Task<string> AddCategoryAsync(CategoryDto category)
        {
            var response = await _client.PostAsJsonAsync("api/Category/addCategory", category);
            return response.IsSuccessStatusCode
                ? "Category added successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}";
        }

        public async Task<string> UpdateCategoryAsync(int id, string newName)
        {
            var updatedCategory = new CategoryDto
            {
                CategoryId = id,
                CategoryName = newName
            };

            var response = await _client.PutAsJsonAsync($"api/Category/{id}/updateCategory", updatedCategory);

            return response.IsSuccessStatusCode
                ? "Category updated successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}";
        }

        public async Task<string> DeleteCategoryAsync(int id)
        {
            var response = await _client.DeleteAsync($"api/Category/{id}/deleteCategory");

            return response.IsSuccessStatusCode
                ? "Category deleted successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}";
        }
    }
}
