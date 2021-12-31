using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class Product
    {
        private int productId;
        private string productName;
        private decimal productPrice;

        public Product(int Id, string Name, decimal Price)
        {
            productId = Id;
            productName = Name;
            productPrice = Price;
        }

        public int GetID()

        {
            return productId;
        }

        public void SetID(int ID)

        {
            productId = ID;
        }

        public string GetName()

        {
            return productName;
        }

        public void SetName(string Name)

        {
            productName = Name;
        }

        public decimal GetPrice()

        {
            return productPrice;
        }

        public void SetPrice(decimal Price)

        {
            productPrice = Price;
        }
    }
}
