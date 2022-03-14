using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class ShoppingCart : Interfaces.IShoppingCart
    {
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        private List<ShoppingCartItem> Products;
        
        public ShoppingCart(Customer cust)
        {
            Customer = cust;
        }


        public int GetCustomerId()
        {
            return Customer.Id;
        }


        public ShoppingCart()
        {
            Products = new List<ShoppingCartItem>();
        }

        

        public ShoppingCartItem GetProductById(int id)
        {
            var ProductbyId = from product in Products
                           where product.Product.Id == id
                           select product;
            return ProductbyId.FirstOrDefault();

        }
        public ShoppingCartItem AddProduct(Product prod, int Quantity) 
        {
            if (Quantity <= 0)
                return null;

            var existingItem = GetProductById(prod.Id);
            if (existingItem == null)
            {
                var newitem = new ShoppingCartItem(prod,Quantity);
                Products.Add(newitem);
                return newitem;
            }

            if (Products.Contains(existingItem))
            {
                existingItem.Quantity += Quantity;
                return existingItem;
            }
            else
                return null;
        }
        
        public ShoppingCartItem RemoveProduct(int id, int Quantity) 
        {
            if (Quantity < 1)
                return null;

            var existingItem = GetProductById(id);
            if (Products.Contains(existingItem) && (existingItem.Quantity - Quantity >= 1))
            {
                existingItem.Quantity -= Quantity;
                return existingItem;
            }
            if (Products.Contains(existingItem) && (existingItem.Quantity - Quantity <= 0))
            {
                existingItem.Quantity = 0;
                Products.Remove(existingItem);
                return existingItem;
            }
            else
                return null;
        }

        public decimal GetTotal() 
        {
            var total = from product in Products
                           let productTotal = product.GetTotal()
                           select productTotal;
            return total.Sum();
            
            
        }

        public List<ShoppingCartItem> GetProducts()
        {
            return Products;

        }

    }
}
