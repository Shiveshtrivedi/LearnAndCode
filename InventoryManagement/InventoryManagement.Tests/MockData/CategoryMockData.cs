using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Tests.MockData
{
    using InventoryManagement.Models;
    using InventoryManagement.Models;
    using System.Collections.Generic;

    public static class CategoryRepositoryMockData
    {
        public static List<Category> Categories => new()
        {
            new Category { CategoryId = 1, CategoryName = "Electronics" },
            new Category { CategoryId = 2, CategoryName = "Clothing" }
        };
    }

}
