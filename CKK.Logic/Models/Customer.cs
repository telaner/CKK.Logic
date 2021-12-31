using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class Customer
    {
        private int customerId;
        private string customerName;
        private string customerAddress;

        public Customer(int Id, string Name, string Address)

        {
            customerId = Id;
            customerName = Name;
            customerAddress = Address;
        }

        public int GetId()

        {
            return customerId;
        }

        public void SetId(int Id)

        {
            customerId = Id;
        }

        public string GetName()

        {
            return customerName;
        }

        public void SetName(string Name)

        {
            customerName = Name;
        }

        public string GetAddress()

        {
            return customerAddress;
        }

        public void SetAddress(string Address)

        {
            customerAddress = Address;
        }

    }
}
