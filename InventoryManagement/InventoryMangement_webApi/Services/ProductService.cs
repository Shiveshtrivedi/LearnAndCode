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

        public OperationResult RegisterProduct(ProductCreateDto productDto)
        {
            var category = _categoryService.GetCategoryById(productDto.CategoryId);
            var supplier = _supplierService.GetSupplierById(productDto.SupplierId);

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

            var result = _productRepository.AddProduct(product);

            if (result.IsSuccess)
            {
                _inventoryService.RegisterInventory(product);
            }

            return result;
        }

        public OperationResult UpdateProduct(ProductUpdateDto productDto)
        {
            var product = _productRepository.GetProductById(dto.ProductId);

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

        public OperationResult RegisterMultipleProducts(List<ProductCreateDto> productDtoList)
        {
            foreach (var proudctDto in productDtoList)
            {
                var result = RegisterProduct(proudctDto);
                if (!result.IsSuccess)
                {
                    return OperationResult.Fail($"Failed to add product: {proudctDto.ProductName}");
                }
            }

            return OperationResult.Success();
        }

        public OperationResult RemoveProduct(int productId)
        {
            return _productRepository.DeleteProduct(productId);
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
