using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Exceptions
{
    public class OperationFailedException : InventoryException
    {
        public OperationFailedException(string operation, string message)
            : base($"Operation '{operation}' failed: {message}") { }
    }
}
