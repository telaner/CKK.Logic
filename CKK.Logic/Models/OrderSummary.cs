using CKK.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class OrderSummary
    {
        public IShoppingCart Cart;

        public OrderSummary(Customer cust) 
        {
        }

        public OrderSummary(IShoppingCart cart) 
        {
            Cart = cart;
        }
    }
}
