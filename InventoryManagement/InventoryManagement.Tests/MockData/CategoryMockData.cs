using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Models;

namespace InventoryManagement.Tests.MockData
{
    public static class CategoryRepositoryMockData
    {
        public static List<Category> Categories => new()
        {
            new Category { CategoryId = 1, CategoryName = "Electronics" },
            new Category { CategoryId = 2, CategoryName = "Clothing" }
        };
    }

}
