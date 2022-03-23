using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Interfaces;

namespace CKK.Logic.Models
{
    public class Product : Entity
    {

        public decimal Price { get; set; }
        public decimal SetPrice() 
        {
            if (Price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Price), "Must be greater than 0");  
            }
            else 
            { 
                return Price; 
            }
        }
       

        public Product() : base() { }

    }
}
