using InventoryManagementConsole.DTOs;
using System.Threading.Tasks;

namespace InventoryManagementConsole.Services.Interfaces
{
    public interface ISupplierService
    {
        Task GetAllSuppliersAsync();
        Task AddSupplierAsync(SupplierDto supplier);
        Task DeleteSupplierAsync(int supplieId);
    }
}
