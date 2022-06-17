using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;

namespace CKK.Logic.Models
{
    [Serializable]
    public class Product : Entity
    {
        private decimal _price;

        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value >= 0)
                {
                    _price = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Must be greater than 0.");
                }
            }
        }

        public Product() : base() { }


    }
}
