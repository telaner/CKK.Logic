using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class ShoppingCartItem : Interfaces.InventoryItem
    {
        
        public ShoppingCartItem(Product product, int quantity) : base(product, quantity) { }     
        
        public decimal GetTotal()
        {
            return Product.Price * Quantity; 
        }

    }
}
