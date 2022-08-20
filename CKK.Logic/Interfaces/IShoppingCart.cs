using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Models;

namespace CKK.Logic.Interfaces
{
    public interface IShoppingCart
    {

        int GetCustomerId();

        ShoppingCartItem AddProduct(Product prod, int Quantity);

        ShoppingCartItem RemoveProduct(int id, int Quantity);

        decimal GetTotal();

        ShoppingCartItem GetProductById(int id);

        List<ShoppingCartItem> GetProducts();
    }
}
