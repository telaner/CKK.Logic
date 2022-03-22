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

        public decimal Price
        {
            get
            {
                return Price;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Price must be 0 or greater");
                }

            }
        }
        public Product() : base() { }

    }
}
