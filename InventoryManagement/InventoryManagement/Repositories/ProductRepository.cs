using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Context;
using InventoryManagement.Utils;
using InventoryManagement.Exceptions;
using InventoryManagement.Enum;

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
            var existingProduct = ProductDb.ProductData.FirstOrDefault(products => products.ProductId == product.ProductId);

            if (existingProduct != null)
            {
                return OperationResult.Fail($"Product with ID {product.ProductId} already exists", ErrorCode.AlreadyExists);
            }

            ProductDb.ProductData.Add(product);

            return OperationResult.Success();
        }


        public OperationResult DeleteProduct(int productId)
        {
            try
            {
                Product product = ProductDb.ProductData.FirstOrDefault(products => products.ProductId == productId);

                if (product == null)
                {
                    return OperationResult.Fail("Supplier already exists.", ErrorCode.AlreadyExists);
                }
                ProductDb.ProductData.Remove(product);
                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("Supplier already exists.", ErrorCode.AlreadyExists);
            }

        }

        public Product UpdateProduct(Product product)
        {
            Product existingProduct = GetProductById(product.ProductId);

            existingProduct.ProductName = product.ProductName;
            existingProduct.ProductDescription = product.ProductDescription;
            existingProduct.QuantityInStock = product.QuantityInStock;
            existingProduct.Price = product.Price;

            return existingProduct!;
        }

        
    }
}
