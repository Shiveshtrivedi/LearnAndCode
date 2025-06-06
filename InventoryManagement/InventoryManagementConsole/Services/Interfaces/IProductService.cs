using InventoryManagementConsole.DTOs;
using System.Threading.Tasks;

namespace InventoryManagementConsole.Services.Interfaces
{
    public interface IProductService
    {
        Task GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int productId);
        Task AddProductAsync(ProductCreateDto newProduct);
        Task AddMultipleProductsAsync(List<ProductCreateDto> products);
        Task UpdateProductAsync(ProductUpdateDto updatedProduct);
        Task DeleteProductAsync(int productId);
    }
}
