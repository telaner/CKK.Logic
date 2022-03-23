using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Exceptions;


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

        

        public ShoppingCartItem AddProduct(Product prod, int Quantity) 
        {
            if (Quantity <= 0)
            {
                return null;
                throw new InventoryItemStockTooLowException();
            }

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
            if (Quantity < 0)
            {
                return null;
                throw new ArgumentOutOfRangeException(nameof(Quantity),"Quantity must be greater than 0");
            }

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
            {
                return null;
                throw new ProductDoesNotExistException();
            }
        }

        public decimal GetTotal() 
        {
            var total = from product in Products
                           let productTotal = product.GetTotal()
                           select productTotal;
            return total.Sum();
                        
        }

        public ShoppingCartItem GetProductById(int id)
        {
            if (id < 0) 
            {
                throw new InvalidIdException();
            }
            var ProductbyId = from product in Products
                              where product.Product.Id == id
                              select product;
            return ProductbyId.FirstOrDefault();

        }

        public List<ShoppingCartItem> GetProducts()
        {
            return Products;

        }

    }
}
