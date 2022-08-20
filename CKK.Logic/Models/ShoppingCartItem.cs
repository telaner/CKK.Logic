using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Interfaces;

namespace CKK.Logic.Models
{
    [Serializable]
    public class ShoppingCartItem : InventoryItem
    {
        public int ShoppingCartId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        
        public ShoppingCartItem(Product product, int quantity) : base(product, quantity) { }     
        
        public decimal GetTotal()
        {
            return base.Product.Price * Quantity; 
        }

    }
}
