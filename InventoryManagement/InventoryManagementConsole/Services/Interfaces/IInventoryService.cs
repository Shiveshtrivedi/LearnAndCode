using InventoryManagementConsole.DTOs;
using System.Threading.Tasks;

namespace InventoryManagementConsole.Services
{
    public interface IInventoryService
    {
        Task UpdateInventoryAsync(int productId, int quantity);
        Task GetAllInventoriesAsync();
        Task GetLowStockItemsAsync();
    }
}
