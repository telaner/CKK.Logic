using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Interfaces;

namespace CKK.Logic.Models
{
    [Serializable]
    public class Customer : Entity
    {
        public int CustomerId { get; set; }

        public int ShoppingCartId { get; set; }

        public ShoppingCart Cart { get; set; }
        
        public string Address { get; set; }

        public Customer() : base() { }

    }
}
