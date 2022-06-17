using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Exceptions
{
    [Serializable]
    public class InventoryItemStockTooLowException : Exception
    {
        public InventoryItemStockTooLowException() : base ("Quantity value cannot be less than 0") 
        {

        }

        public InventoryItemStockTooLowException(string message) : base (message) 
        {

        }

        public InventoryItemStockTooLowException(string message, Exception inner) : base(message) 
        {

        }
    }
}
