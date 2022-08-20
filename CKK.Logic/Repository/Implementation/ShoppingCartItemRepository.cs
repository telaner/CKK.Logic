using CKK.Logic.Models;
using CKK.Logic.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Repository.Implementation
{
    public class ShoppingCartItemRepository : IShoppingCartItemRepository
    {
        public ShoppingCartItem AddtoCart(int itemId, int quantity)
        {
            throw new NotImplementedException();
        }

        public ShoppingCartItem AddtoCart(string itemName, int quantity)
        {
            throw new NotImplementedException();
        }

        public int GetCustomerId(int shoppingCartId)
        {
            throw new NotImplementedException();
        }

        public List<ShoppingCartItem> GetProducts(int shoppingCartId)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotal(int ShoppingCartId)
        {
            throw new NotImplementedException();
        }

        public ShoppingCartItem RemoveFromCart(int shoppingCartId, int itemId, int quantity = 1)
        {
            throw new NotImplementedException();
        }
    }
}
