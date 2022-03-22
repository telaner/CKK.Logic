using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Exceptions;


namespace CKK.Logic.Interfaces
{
    public abstract class InventoryItem
    {
        private Product product;
        private int quantity;

        public InventoryItem(Product product, int quantity)
        {
            Prod = product;
            Quantity = quantity;
        }

        public int Quantity 
        {
            get 
            { 
                return quantity; 
            }
            set 
            {
                if (value < 0) 
                {
                    throw new InventoryItemStockTooLowException();
                }
                quantity = value;
            }
            
        }

        public Product Prod 
        {
            get 
            {
                return product;
            }

            set 
            {
                product = value; 
            }

        }
    }
}
