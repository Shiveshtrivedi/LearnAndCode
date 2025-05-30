using System.Threading.Tasks;

namespace InventoryManagementConsole.Services
{
    public interface ISupplierService
    {
        Task GetAllSuppliersAsync();
        Task AddSupplierAsync();
        Task DeleteSupplierAsync();
    }
}
