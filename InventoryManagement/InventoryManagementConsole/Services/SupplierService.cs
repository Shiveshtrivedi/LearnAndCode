using System.Net.Http.Json;
using InventoryManagementConsole.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagementConsole.Services.Interfaces;

namespace InventoryManagementConsole.Services
{
    public class SupplierService : ISupplierService
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
            if(suppliers.Count==0)
            {
                Console.WriteLine("No Supplier present");
            }
            else
            {
                foreach (var supplier in suppliers)
                {
                    Console.WriteLine($"S.ID : {supplier.SupplierId} | Name : {supplier.SupplierName} | Contact : {supplier.ContactNumber}");
                }
            }
        }

        public async Task AddSupplierAsync(SupplierDto supplier)
        {
            var response = await _client.PostAsJsonAsync("api/Supplier/addSupplier", supplier);

            Console.WriteLine(response.IsSuccessStatusCode
                ? "Supplier added successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}");
        }

        public async Task DeleteSupplierAsync(int supplieId)
        {
            var response = await _client.DeleteAsync($"api/Supplier/{supplieId}/deleteSupplier");

            Console.WriteLine(response.IsSuccessStatusCode
                ? "Supplier deleted successfully."
                : $"Failed: {await response.Content.ReadAsStringAsync()}");
        }
    }
}
