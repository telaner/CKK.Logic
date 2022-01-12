using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class ShoppingCartItem
    {
        private Product shoppingProduct;
        private int shoppingQuantity;
        private decimal productPrice;
        
              

        public ShoppingCartItem(Product product, int Quantity, decimal Price)

        {
            shoppingProduct = product;
            shoppingQuantity = Quantity;
            productPrice = Price;
        }

        public Product GetProduct()

        {
            return shoppingProduct;
        }

        public void SetProduct(Product product)

        {
            shoppingProduct = product;
        }

        public decimal GetPrice() 
        
        {
            return productPrice;       
        }

        public void SetPrice(int Price)

        {
            productPrice = Price;
        }

        public int GetQuantity()

        {
            return shoppingQuantity;
        }

        public void SetQuantity(int Quantity)

        {
            shoppingQuantity = Quantity;
        }
        
        public decimal GetTotal()
        {
            return shoppingQuantity * productPrice;
        }

    }
}
