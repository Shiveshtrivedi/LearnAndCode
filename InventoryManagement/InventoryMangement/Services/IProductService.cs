
using InventoryManagement.DTOs;
using InventoryManagement.Models;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryMangement.DTOs;

namespace InventoryManagement.Services
{
    public interface IProductService
    {
        Product GetProductById(int id);
        Product GetProductByName(string productName);
        IEnumerable<Product> GetAllProducts();
        OperationResult UpdateProduct(ProductUpdateDto dto);
        string RegisterProduct(ProductCreateDto dto);
        void RemoveProduct(int productId);
        void RegisterMultipleProducts(List<ProductCreateDto> dtoList);

    }
}
