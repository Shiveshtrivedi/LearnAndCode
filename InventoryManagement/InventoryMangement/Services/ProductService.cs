using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;
using InventoryManagement.Utils;
using InventoryManagement.DTOs;
using InventoryMangement.DTOs;

namespace InventoryManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        private readonly IInventoryService _inventoryService;

        public ProductService(
            IProductRepository productRepository,
            ICategoryService categoryService,
            ISupplierService supplierService,
            IInventoryService inventoryService)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _inventoryService = inventoryService;
        }

        public string RegisterProduct(ProductCreateDto productDto)
        {
            string resultMessage = "Product registered successfully.";

            var category = _categoryService.GetOrCreateUncategorizedCategory(productDto.CategoryId);

            if (category.CategoryId != productDto.CategoryId)
            {
                resultMessage = $"Category not found. Product assigned to 'Uncategorized'.";
            }

            var supplier = _supplierService.GetOrCreateDefaultSupplier(productDto.SupplierId);

            if (supplier.SupplierId != productDto.SupplierId)
            {
                if (resultMessage == "Product registered successfully.")
                    resultMessage = "Supplier not found. Product assigned to 'Default Supplier'.";
                else
                    resultMessage += " Supplier not found. Product assigned to 'Default Supplier'.";
            }

            var product = new Product
            {
                ProductId = IdGenerator.GetNextId(),
                ProductName = productDto.ProductName,
                ProductDescription = productDto.ProductDescription,
                QuantityInStock = productDto.QuantityInStock,
                Price = productDto.Price,
                CategoryId = category.CategoryId,
                Category = category,
                SupplierId = supplier.SupplierId,
                Supplier = supplier
            };

            _productRepository.AddProduct(product);
            _inventoryService.RegisterInventory(product);

            return resultMessage;
        }



        public OperationResult UpdateProduct(ProductUpdateDto productDto)
        {
            var product = _productRepository.GetProductById(productDto.ProductId);

            product.ProductName = productDto.ProductName;
            product.ProductDescription = productDto.ProductDescription;
            product.QuantityInStock = productDto.QuantityInStock;
            product.Price = productDto.Price;
            product.CategoryId = productDto.CategoryId;
            product.SupplierId = productDto.SupplierId;

            var updatedProduct = _productRepository.UpdateProduct(product);
            _inventoryService.UpdateInventory(productDto.ProductId, productDto.QuantityInStock);

            return OperationResult.Success();
        }

        public void RegisterMultipleProducts(List<ProductCreateDto> productDtoList)
        {
            foreach (var productDto in productDtoList)
            {
                try
                {
                    RegisterProduct(productDto);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to register product: {productDto.ProductName}", ex);
                }
            }
        }

        public void RemoveProduct(int productId)
        {
             _productRepository.DeleteProduct(productId);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public Product GetProductById(int productId)
        {
            return _productRepository.GetProductById(productId);
        }

        public Product GetProductByName(string productName)
        {
            return _productRepository.GetProductByName(productName);
        }

    }
}
