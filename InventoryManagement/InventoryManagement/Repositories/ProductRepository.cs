using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Context;
using InventoryManagement.Utils;

namespace InventoryManagement.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Product FindProduct(int id)
        {
            Product product = ProductDb.ProductData.FirstOrDefault(products => products.ProductId == id);
            return product;
        }
        public OperationResult AddProduct(Product product)
        {
            var existingProduct = FindProduct(product.ProductId); 

            if (existingProduct != null)
            {
                return new OperationResult { IsSuccess = false , ErrorMessage="Product already exist"};
            }
            ProductDb.ProductData.Add(product);

            return new OperationResult { IsSuccess = true };
        }

        public OperationResult DeleteProduct(int productId)
        {
           Product product = FindProduct(productId);
            if (product != null) 
            { 
                ProductDb.ProductData.Remove(product);
                return new OperationResult { IsSuccess = true };
            }

            return new OperationResult { IsSuccess = false , ErrorMessage="Product Not Found" };
        }

        public IEnumerable<Product> GetAllProducts()
        {
           return ProductDb.ProductData;
        }

        public Product GetProductById(int productId)
        {
            Product product = FindProduct(productId);
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            Product existingProduct = FindProduct(product.ProductId);

            if (existingProduct != null) 
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductDescription = product.ProductDescription;
                existingProduct.QuantityInStock = product.QuantityInStock;
                existingProduct.Price = product.Price;
            }


            return existingProduct;
        }

        
    }
}
