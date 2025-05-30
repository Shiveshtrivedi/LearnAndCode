using System.Threading.Tasks;

namespace InventoryManagementConsole.Services
{
    public interface IInventoryService
    {
        Task RegisterInventoryAsync();
        Task UpdateInventoryAsync();
        Task GetAllInventoriesAsync();
        Task GetLowStockItemsAsync();
    }
}
