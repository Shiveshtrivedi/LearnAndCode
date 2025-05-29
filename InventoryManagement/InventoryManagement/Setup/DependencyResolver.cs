using InventoryManagement.Repositories;
using InventoryManagement.Services;
using InventoryManagement.Utils.Interfaces;
using InventoryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Setup
{
    public static class DependencyResolver
    {
        public static (IProductService productService, IInventoryService inventoryService) ResolveDependencies()
        {
            IProductRepository productRepo = new ProductRepository();
            ICategoryRepository categoryRepo = new CategoryRepository();
            IInventoryRepository inventoryRepo = new InventoryRepository();

            ICategoryService categoryService = new CategoryService(categoryRepo);
            ICategoryInputHelper categoryInput = new CategoryInputHelper();
            ISupplierInputHelper supplierInput = new SupplierInputHelper();
            IProductInputHelper productInput = new ProductInputHelper(categoryInput, supplierInput);
            IInventoryService inventoryService = new InventoryService(inventoryRepo, productRepo);
            IProductService productService = new ProductService(productRepo, categoryService, inventoryService, productInput);

            return (productService, inventoryService);
        }
    }
}
