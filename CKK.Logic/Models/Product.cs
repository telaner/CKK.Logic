using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class Product : Interfaces.Entity
    {

        public decimal Price { get; set; }
        public Product(int Id, string name) : base(Id, name) { }

    }
}
