using InventoryManagement.Context;
using InventoryManagement.Repositories;
using InventoryManagement.Tests.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Tests
{
    public class ProductRepositoryTests
    {
        private readonly ProductRepository _repository;

        public ProductRepositoryTests()
        {
            ProductDb.ProductData = ProductMockData.GetMockProducts();
            _repository = new ProductRepository();
        }

        [Fact]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var result = _repository.GetAllProducts().ToList();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetProductById_ValidId_ShouldReturnProduct()
        {
            var result = _repository.GetProductById(1);

            Assert.Equal("Laptop", result.ProductName);
        }

        [Fact]
        public void GetProductByName_ValidName_ShouldReturnProduct()
        {
            var result = _repository.GetProductByName("Mouse");

            Assert.Equal(2, result.ProductId);
        }

        [Fact]
        public void AddProduct_NewProduct_ShouldAddSuccessfully()
        {
            var newProduct = ProductMockData.GetNewProduct();
            var result = _repository.AddProduct(newProduct);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void DeleteProduct_ExistingProduct_ShouldDeleteSuccessfully()
        {
            var result = _repository.DeleteProduct(1);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void UpdateProduct_ValidProduct_ShouldUpdateFields()
        {
            var product = _repository.GetProductById(2);
            product.ProductDescription = "Updated Mouse";
            product.QuantityInStock = 100;

            var updated = _repository.UpdateProduct(product);

            Assert.Equal("Updated Mouse", updated.ProductDescription);
        }

        [Fact]
        public void UpdateProductQuantity_ShouldChangeQuantityOnly()
        {
            var updated = _repository.UpdateProductQuantity(1, 999);

            Assert.Equal(999, updated.QuantityInStock);
        }
    }
}
