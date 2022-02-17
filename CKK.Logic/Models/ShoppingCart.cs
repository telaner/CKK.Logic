using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class ShoppingCart
    {
        private Customer _customer;
        private List<ShoppingCartItem> Products;


        public ShoppingCart(Customer cust)
        {
            Products = new List<ShoppingCartItem>();
            _customer = cust;
        }

        
        public int GetCustomerID() 
        {
            return _customer.GetId();
        }

        public ShoppingCartItem GetProductById(int id)
        {
            var ProductbyId = from product in Products
                           where product.GetProduct().GetId() == id
                           select product;
            return ProductbyId.FirstOrDefault();

        }
        public ShoppingCartItem AddProduct(Product prod, int Quantity) 
        {
            if (Quantity < 1)
                return null;

            var existingItem = GetProductById(prod.GetId());
            if (existingItem == null)
            {
                Products.Add(existingItem);
                return existingItem;
            }

            if (Products.Contains(existingItem))
            {
                existingItem.SetQuantity(existingItem.GetQuantity() + Quantity);
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
            if (Products.Contains(existingItem) && (existingItem.GetQuantity() - Quantity >= 0))
            {
                existingItem.SetQuantity(existingItem.GetQuantity() - Quantity);
                return existingItem;
            }
            if (Products.Contains(existingItem) && (existingItem.GetQuantity() - Quantity <= 0))
            {
                Products.Remove(existingItem);
                existingItem.SetProduct(null);
                existingItem.SetQuantity(0);
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
