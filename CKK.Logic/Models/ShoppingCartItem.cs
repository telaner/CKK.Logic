﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Interfaces;

namespace CKK.Logic.Models
{
    public class ShoppingCartItem : InventoryItem
    {
        
        public ShoppingCartItem(Product Prod, int Quantity) : base(Prod, Quantity) { }     
        
        public decimal GetTotal()
        {
            return base.Prod.Price * Quantity; 
        }

    }
}
