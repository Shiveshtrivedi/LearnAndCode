using InventoryManagement.Models;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repositories
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();
        public Product GetProductById(int id);
        Product GetProductByName(string productName);
        public OperationResult AddProduct(Product product);
        public Product UpdateProduct(Product product);
        Product UpdateProductQuantity(int productId, int quantity);

        public OperationResult DeleteProduct(int id);
             
    }
}
