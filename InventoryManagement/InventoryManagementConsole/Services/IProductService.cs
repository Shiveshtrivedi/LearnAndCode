using System.Threading.Tasks;

namespace InventoryManagementConsole.Services
{
    public interface IProductService
    {
        Task GetAllProductsAsync();
        Task AddProductAsync();
        Task UpdateProductAsync();
        Task DeleteProductAsync();
    }
}
