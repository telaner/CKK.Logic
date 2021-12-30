using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class Customer
    {
        private int customerID;
        private string customerName;
        private string customerAddress;

        public Customer(int ID, string Name, string Address)

        {
            customerID = ID;
            customerName = Name;
            customerAddress = Address;
        }

        public int GetID()

        {
            return customerID;
        }

        public void SetID(int ID)

        {
            customerID=ID;
        }

        public string GetName()

        {
            return customerName;
        }

        public void SetName(string Name)

        {
            customerName=Name;
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
