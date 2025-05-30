using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagement.Context;
using InventoryManagement.Exceptions;
using InventoryManagement.Enum;
using InventoryManagement.Utils;

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
            var product = ProductDb.ProductData.FirstOrDefault(product => product.ProductId == productId);

            if (product == null)
            {
                throw new ProductNotFoundException(productId);
            }

            return product;
        }

        public Product GetProductByName(string productName)
        {
            var product = ProductDb.ProductData.FirstOrDefault(product => product.ProductName == productName);
            return product;          
        }

        public OperationResult AddProduct(Product product)
        {
            var existingProduct = ProductDb.ProductData.FirstOrDefault(p => p.ProductId == product.ProductId);

            if (existingProduct != null)
            {
                return OperationResult.Fail($"Product with ID {product.ProductId} already exists", ErrorCode.AlreadyExists);
            }

            ProductDb.ProductData.Add(product);

            return OperationResult.Success();
        }

        public OperationResult DeleteProduct(int productId)
        {
            var product = ProductDb.ProductData.FirstOrDefault(product => product.ProductId == productId);

            if (product == null)
            {
                return OperationResult.Fail($"Product with ID {productId} not found.", ErrorCode.NotFound);
            }

            ProductDb.ProductData.Remove(product);
            return OperationResult.Success();
        }

        public Product UpdateProduct(Product product)
        {
            var existingProduct = GetProductById(product.ProductId);

            existingProduct.ProductName = product.ProductName;
            existingProduct.ProductDescription = product.ProductDescription;
            existingProduct.QuantityInStock = product.QuantityInStock;
            existingProduct.Price = product.Price;

            return existingProduct;
        }

        public Product UpdateProductQuantity(int productId, int quantity)
        {
            var existingProduct = GetProductById(productId);

            existingProduct.QuantityInStock = quantity;

            return existingProduct;
        }
    }
}
