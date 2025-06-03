using System.Net.Http.Json;
using InventoryManagementConsole.DTOs;

namespace InventoryManagementConsole.Services
{
    public class SupplierService
    {
        private readonly HttpClient _client;

        public SupplierService(IHttpClientFactoryWrapper clientFactoryWrapper)
        {
            _client = clientFactoryWrapper.GetClient();
        }

        public async Task GetAllSuppliersAsync()
        {
            var suppliers = await _client.GetFromJsonAsync<List<SupplierDto>>("api/Supplier/getAllSupplier");

            Console.WriteLine("=== Suppliers ===");
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"{supplier.SupplierId} | {supplier.SupplierName} | {supplier.ContactNumber}");
            }
        }

        public async Task AddSupplierAsync()
        {
            Console.Write("Enter Supplier Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Contact Number: ");
            string contact = Console.ReadLine();

            var supplier = new SupplierDto
            {
                SupplierName = name,
                ContactNumber = contact,
            };

            var response = await _client.PostAsJsonAsync("api/Supplier/addSupplier", supplier);

            Console.WriteLine(response.IsSuccessStatusCode
                ? "Supplier added successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}");
        }

        public async Task DeleteSupplierAsync()
        {
            Console.Write("Enter Supplier ID to delete: ");
            int id = int.Parse(Console.ReadLine());

            var response = await _client.DeleteAsync($"api/Supplier/{id}/deleteSupplier");

            Console.WriteLine(response.IsSuccessStatusCode
                ? "Supplier deleted successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}");
        }
    }
}
