
using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Services
{
    public interface IProductService
    {
        Product GetProductById(int id);
        Product GetProductByName(string productName);
        IEnumerable<Product> GetAllProducts();
        void RegisterProduct();
        void RegisterMultipleProduct();
        void UpdateProductDetails();
        void RemoveProduct(int productId);

    }
}
