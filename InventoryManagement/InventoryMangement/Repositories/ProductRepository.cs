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
            if(ProductDb.ProductData.Count == 0)
            {
                throw new ProductNotFoundException();
            }
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
            if (product == null)
            {
                throw new ProductNotFoundException();
            }
            return product;          
        }

        public void AddProduct(Product product)
         {
            var existingProduct = ProductDb.ProductData.FirstOrDefault(p => p.ProductId == product.ProductId);

            if (existingProduct != null)
            {
                throw new InvalidOperationException($"Product with ID {product.ProductId} already exists.");
            }

            ProductDb.ProductData.Add(product);
        }

        public void DeleteProduct(int productId)
        {
            var product = ProductDb.ProductData.FirstOrDefault(product => product.ProductId == productId);

            if (product == null)
            {
                throw new ProductNotFoundException(productId);
            }

            ProductDb.ProductData.Remove(product);
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
