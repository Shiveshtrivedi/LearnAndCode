using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;
using InventoryManagement.Utils;
using InventoryManagement.Utils.Interfaces;
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
       private readonly IProductInputHelper _productInputHelper;


        public ProductService(IProductRepository productRepository, ICategoryService categoryService, IInventoryService inventoryService, IProductInputHelper productInputHelper) 
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
            _inventoryService = inventoryService;
            _productInputHelper = productInputHelper;
        }
        public void AddProduct()
        {

            Product product = _productInputHelper.GetInputFromUser(_categoryService,_supplierService);

            OperationResult result = _productRepository.AddProduct(product);
            

            if (result.IsSuccess)
            {
                OperationResult inventoryResult = _inventoryService.AddInventory(product);
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

            if (!result.IsSuccess)
            {
                throw new OperationFailedException("Delete Product", result.ErrorMessage);
            }

            Console.WriteLine("Product deleted successfully");
        } 

        public IEnumerable<Product> GetAllProducts()
        {
            IEnumerable<Product> products = _productRepository.GetAllProducts();

            return products;
        }

        public Product GetProductById(int productId)
        {
            Product product = _productRepository.GetProductById(productId);

            return product;
        }

        public void UpdateProduct()
        {
            
            Product product = _productInputHelper.GetInputFromUser(_categoryService,_supplierService,isUpdate:true);

            Product updatedProduct = _productRepository.UpdateProduct(product);

            //OperationResult inventoryResult = _inventoryService.UpdateInventory(product);

            Console.WriteLine("Product Updated Successfully");

        }
    }
}
