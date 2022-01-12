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
                    
        public ShoppingCartItem(Product product, int Quantity)

        {
            shoppingProduct = product;
            shoppingQuantity = Quantity;
        }

        public Product GetProduct()

        {
            return shoppingProduct;
        }

        public void SetProduct(Product product)

        {
            shoppingProduct = product;
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
            return GetProduct().GetPrice() * shoppingQuantity;
        }

    }
}
