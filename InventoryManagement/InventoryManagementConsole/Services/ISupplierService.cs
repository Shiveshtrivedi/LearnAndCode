using InventoryManagementConsole.DTOs;
using System.Threading.Tasks;

namespace InventoryManagementConsole.Services
{
    public interface ISupplierService
    {
        Task GetAllSuppliersAsync();
        Task AddSupplierAsync(SupplierDto supplier);
        Task DeleteSupplierAsync(int supplieId);
    }
}
