using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Utils
{
    public class IdGenerator
    {
        private static int id = 1;

        public static int GetNextId()
        {
            return id++;
        }
    }
}
