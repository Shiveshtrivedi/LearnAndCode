using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Services
{
    public class ProductService : IProductService
    {
       private readonly IProductRepository _productRepository;
       private readonly ICategoryService _categoryService;
       private readonly ISupplierService _supplierService;
        private readonly IInventoryService _inventoryService;

        public ProductService(IProductRepository productRepository, ICategoryService categoryService, IInventoryService inventoryService) 
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
            _inventoryService = inventoryService;
        }
        public void AddProduct()
        {

            Product product = ProductInputHelper.GetInputFromUser(_categoryService,_supplierService);

            OperationResult result = _productRepository.AddProduct(product);
            
            OperationResult inventoryResult = _inventoryService.AddInventory(product);

            if (result.IsSuccess)
            {
                Console.WriteLine("Product added successfully");
            }
            else
            {
                Console.WriteLine($"Failed to add product : {result.ErrorMessage}");
            }
        }

        public void AddMultipleProduct()
        {
            while(true)
            {
                AddProduct();
                Console.Write("Do you want add another Product? (yes/no)");
                string exit = Console.ReadLine();
                if (exit!="yes" )
                {
                    break;
                }
            }
        }

        public void DeleteProduct(int productId)
        {
            OperationResult result = _productRepository.DeleteProduct(productId);

            if (result.IsSuccess)
            {
                Console.WriteLine("Product deleted successfully");
            }
            else
            {
                Console.WriteLine($"while deleting {result.ErrorMessage}");
            }
            
        } 

        public IEnumerable<Product> GetAllProducts()
        {
            IEnumerable<Product> products = _productRepository.GetAllProducts();

            return products;
        }

        public Product GetProductById(int productId)
        {
            Product product = _productRepository.GetProductById(productId);

            if(product == null)
            {
                throw new ProductNotFoundException(productId);
            }

            return product;
        }

        public void UpdateProduct()
        {
            
            Product product = ProductInputHelper.GetInputFromUser(_categoryService,_supplierService,isUpdate:true);

            Product updatedProduct = _productRepository.UpdateProduct(product);

            //OperationResult inventoryResult = _inventoryService.UpdateInventory(product);

            Console.WriteLine("Product Updated Successfully");

        }
    }
}
