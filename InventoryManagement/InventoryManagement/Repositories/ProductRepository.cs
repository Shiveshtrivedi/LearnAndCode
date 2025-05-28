using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Context;
using InventoryManagement.Utils;
using InventoryManagement.Exceptions;

namespace InventoryManagement.Repositories
{
    public class ProductRepository : IProductRepository
    {

        public IEnumerable<Product> GetAllProducts()
        {
            return ProductDb.ProductData;
        }

        public Product GetProductById(int productId)
        {
            Product product = ProductDb.ProductData.FirstOrDefault(products => products.ProductId == productId);

            if (product == null)
            {
                throw new ProductNotFoundException(productId);
            }

            return product;
        }
        public OperationResult AddProduct(Product product)
        {
            try
            {
                var existingProduct = GetProductById(product.ProductId);

                ProductDb.ProductData.Add(product);

                return new OperationResult { IsSuccess = true };
            }
            catch (Exception ex) 
            {
                return new OperationResult { IsSuccess = false, ErrorMessage = "Supplier already exists" };
            }
        }


        public OperationResult DeleteProduct(int productId)
        {
            try
            {
                Product product = GetProductById(productId);
                ProductDb.ProductData.Remove(product);
                return new OperationResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new OperationResult { IsSuccess = false , ErrorMessage= $"Product Not Found {ex.Message}" };
            }

        }

        public Product UpdateProduct(Product product)
        {
            Product existingProduct = GetProductById(product.ProductId);

            if (existingProduct != null) 
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductDescription = product.ProductDescription;
                existingProduct.QuantityInStock = product.QuantityInStock;
                existingProduct.Price = product.Price;
            }


            return existingProduct!;
        }

        
    }
}
