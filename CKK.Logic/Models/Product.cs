using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    internal class Product
    {
        private int productID;
        private string productName;
        private decimal productPrice;

        public Product(int ID, string Name, decimal Price)
        {
            productID = ID;
            productName = Name;
            productPrice = Price;
        }

        public int GetID()

        {
            return productID;
        }

        public void SetID(int ID)

        {
            productID=ID;
        }

        public string GetName()

        {
            return productName;
        }

        public void SetName(string Name)

        {
            productName=Name;
        }

        public decimal GetPrice()

        {
            return productPrice;
        }

        public void SetPrice(decimal Price)

        {
            productPrice=Price;
        }
    }
}
