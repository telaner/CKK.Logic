using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CKK.Logic.Repository.Implementation;

namespace CKK.Logic.Models
{
    [Serializable]
    public class ShoppingCartItem 
    {
        public Product Product { get; set; }
        
        public int ShoppingCartId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public decimal GetTotal()
        {
            return Product.Price * Quantity;
        }

    }
}
