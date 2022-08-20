using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Exceptions;


namespace CKK.Logic.Models
{
    [Serializable]
    public class ShoppingCart 
    {
        public int ShoppingCartId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        //public int ShoppingCartId { get; set; }
        
        //public int CustomerId { get; set; }
        //public Product Product { get; set; }
        //public List<ShoppingCartItem> ShoppingCartItems{ get; set; } = new List<ShoppingCartItem>();
        
        //public ShoppingCart(Customer cust)
        //{
        //    Customer = cust;
            
        //}                 
        
        //public ShoppingCartItem AddProduct(Product prod, int Quantity) 
        //{
        //    if (Quantity <= 0)
        //    {

        //        throw new InventoryItemStockTooLowException();
        //    }

        //    var existingItem = GetProductById(prod.Id);
        //    if (existingItem == null)
        //    {
        //        var newitem = new ShoppingCartItem(prod,Quantity);
        //        ShoppingCartItems.Add(newitem);
        //        return newitem;
        //    }

        //    if (ShoppingCartItems.Contains(existingItem))
        //    {
        //        existingItem.Quantity += Quantity;
        //        return existingItem;
        //    }
        //    else
        //        return null;
        //}
        
        //public ShoppingCartItem RemoveProduct(int id, int Quantity) 
        //{
        //    if (Quantity < 0)
        //    {
                
        //        throw new ArgumentOutOfRangeException(nameof(Quantity),"Quantity must be greater than 0");
        //    }

        //    var existingItem = GetProductById(id);
        //    if (ShoppingCartItems.Contains(existingItem) && (existingItem.Quantity - Quantity >= 1))
        //    {
        //        existingItem.Quantity -= Quantity;
        //        return existingItem;
        //    }
        //    if (ShoppingCartItems.Contains(existingItem) && (existingItem.Quantity - Quantity <= 0))
        //    {
        //        existingItem.Quantity = 0;
        //        ShoppingCartItems.Remove(existingItem);
        //        return existingItem;
        //    }
        //    if (!ShoppingCartItems.Contains(existingItem)) 
        //    {
        //        throw new ProductDoesNotExistException();
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public decimal GetTotal() 
        //{
        //    var total = from product in ShoppingCartItems
        //                   let productTotal = product.GetTotal()
        //                   select productTotal;
        //    return total.Sum();
                        
        //}

        //public ShoppingCartItem GetProductById(int id)
        //{
        //    if (id < 0) 
        //    {
        //        throw new InvalidIdException();
        //    }
        //    var ProductbyId = from product in ShoppingCartItems
        //                      where product.Product.Id == id
        //                      select product;
        //    return ProductbyId.FirstOrDefault();

        //}

        //public List<ShoppingCartItem> GetProducts()
        //{
        //    return ShoppingCartItems;

        //}

    }
}
