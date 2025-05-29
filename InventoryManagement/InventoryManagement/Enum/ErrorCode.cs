using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Enum
{
    public enum ErrorCode
    {
        None = 0,
        NotFound = 1,
        AlreadyExists = 2,
        ValidationError = 3,
        DatabaseError = 4,
        UnknownError = 5
    }
}
