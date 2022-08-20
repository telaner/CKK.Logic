using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string OrderNumber { get; set; }
        public int ShoppingCartId { get; set; }

    }
}
