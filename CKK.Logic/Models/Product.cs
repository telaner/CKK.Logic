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

        public int GetId()

        {
            return productId;
        }

        public void SetId(int Id)

        {
            productId = Id;
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
