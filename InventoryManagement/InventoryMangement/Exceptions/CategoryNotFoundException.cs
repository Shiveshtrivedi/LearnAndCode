using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Exceptions
{
    public class CategoryNotFoundException : InventoryException
    {
        public CategoryNotFoundException(int categoryId=0) 
            : base($"Category with this Id {categoryId} not found")
        { }
    }
}
