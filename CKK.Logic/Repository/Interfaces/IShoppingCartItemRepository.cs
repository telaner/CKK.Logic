using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Repository.Interfaces
{
    public interface IShoppingCartItemRepository
    {
        public int GetCustomerId(int shoppingCartId);
        public ShoppingCartItem AddtoCart(int itemId, int quantity);
        public ShoppingCartItem AddtoCart(string itemName, int quantity);
        public ShoppingCartItem RemoveFromCart(int shoppingCartId,int itemId, int quantity=1);
        public decimal GetTotal(int ShoppingCartId);
        public List<ShoppingCartItem> GetProducts(int shoppingCartId);
        public void Ordered(int shoppingCartId);
    }
}
