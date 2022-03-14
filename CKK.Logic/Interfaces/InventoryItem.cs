using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CKK.Logic.Interfaces
{
    public abstract class InventoryItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        protected InventoryItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
