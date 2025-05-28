using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Exceptions
{
    public class InvalidProductException : Exception
    {
        public InvalidProductException(string message)
            : base(message)
        {
        }
    }
}
