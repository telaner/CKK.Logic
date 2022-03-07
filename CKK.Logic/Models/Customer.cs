using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class Customer : Interfaces.Entity
    {
        
        public string Address { get; set; }

        public Customer(int Id, string name) : base (Id, name) { }

    }
}
