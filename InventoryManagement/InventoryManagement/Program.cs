using InventoryManagement.Models;
using InventoryManagement.Repositories;
using InventoryManagement.Services;
using InventoryManagement.Setup;
using InventoryManagement.Utils;
using InventoryManagement.Utils.Interfaces;
using System;

namespace InventoryManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var (productService, inventoryService) = DependencyResolver.ResolveDependencies();

            var menuRouter = new MenuRouter(productService, inventoryService);
            menuRouter.ShowMenu();
        }
    }
}
