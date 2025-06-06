using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Exceptions
{
    public class ProductNotFoundException : InventoryException
    {
        public ProductNotFoundException(int productId = 0) : base($"Product with ID {productId} was not found")
        {
            
        }
    }
}
