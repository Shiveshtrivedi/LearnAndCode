using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class InventoryTransaction
    {
        public int TransctionId { get; set; }
        public int ProductId { get; set; }
        public string TransactionType { get; set; } = "";
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

    }
}
