using CKK.Logic.Models;
using CKK.Logic.Repository.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Repository.Implementation
{
    public class ShoppingCartItemRepository : IShoppingCartItemRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        
        private readonly string _shopcart = "ShoppingCarts";
        


        public ShoppingCartItemRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            
        }
        
        public ShoppingCartItem AddtoCart(int itemId, int quantity)
        {
            using(var conn = _connectionFactory.GetConnection) 
            {
                ProductRepository _productRepository = new ProductRepository(_connectionFactory);
                var item = _productRepository.Find(itemId);
                var shopitem = new ShoppingCartItem();
                shopitem.ProductId = item.Id;
                shopitem.Quantity = quantity;
                shopitem.CustomerId = 1;
                shopitem.ShoppingCartId = 1;
                List<ShoppingCartItem> items = new List<ShoppingCartItem>();
                items.Add(shopitem);
                SqlMapper.Execute(conn, "dbo.ShoppingCart_Insert @ShoppingCartId, @CustomerId, @ProductId, @Quantity", items);
                if (item.Quantity >= quantity) 
                {
                    item.Quantity = item.Quantity - quantity;
                    _productRepository.Update(item);
                }
                
                return shopitem;
            }
            
        }

        public ShoppingCartItem AddtoCart(string itemName, int quantity)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                ProductRepository _productRepository = new ProductRepository(_connectionFactory);
                var product = _productRepository.GetItemsByName(itemName);
                var item = product as Product;
                var shopitem = new ShoppingCartItem();
                shopitem.ProductId = item.Id;
                shopitem.Quantity = quantity;
                shopitem.ShoppingCartId = 1;
                shopitem.CustomerId=1;
                List<ShoppingCartItem> items = new List<ShoppingCartItem>();
                items.Add(shopitem);
                SqlMapper.Execute(conn, "dbo.ShoppingCart_Insert @ShoppingCartId, @CustomerId, @ProductId, @Quantity", items);
                if (item.Quantity >= quantity)
                {
                    item.Quantity = item.Quantity - quantity;
                    _productRepository.Update(item);
                }
                return shopitem;
            }
        }

        public int GetCustomerId(int shoppingCartId)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                var result = SqlMapper.QuerySingleOrDefault<ShoppingCartItem>(conn,$"dbo.ShoppingCarts_shoppingcartid @ShoppingCartId", new {ShoppingCartId = shoppingCartId});
                if (result == null)
                    throw new KeyNotFoundException($"{_shopcart} with id [{shoppingCartId}] could not be found.");
                return result.CustomerId;
            }
        }

        public List<ShoppingCartItem> GetProducts(int shoppingCartId)
        {
            using (var conn = _connectionFactory.GetConnection) 
            {
                var result = SqlMapper.Query<ShoppingCartItem>(conn, $"dbo.ShoppingCarts_shoppingcartid @ShoppingCartId", new { ShoppingCartId = shoppingCartId }).ToList();
                return result;
            }
        }

        public decimal GetTotal(int shoppingCartId)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                var items = SqlMapper.Query<ShoppingCartItem>(conn, $"dbo.ShoppingCarts_shoppingcartid @ShoppingCartId", new { ShoppingCartId = shoppingCartId }).ToList();
                List<decimal> total = new List<decimal>();
               foreach (var item in items) 
                {
                    var price = item.Product.Price;
                    total.Add(price);
                }
               return total.Sum();
               
            }
        }

        public ShoppingCartItem RemoveFromCart(int shoppingCartId, int itemId, int quantity = 1)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                ProductRepository _productRepository = new ProductRepository(_connectionFactory);
                var items = SqlMapper.Query<ShoppingCartItem>(conn, $"dbo.ShoppingCarts_shoppingcartid @ShoppingCartId", new { ShoppingCartId = shoppingCartId }).ToList();
                var shopitem = from item in items
                               where item.ProductId==itemId
                               select item;
                var product = shopitem.FirstOrDefault();
                var id = product.ProductId;
                              



                if (product != null) 
                {

                    product.Quantity = product.Quantity - quantity;
                    if (product.Quantity - quantity> 0) 
                    {
                        SqlMapper.Query<ShoppingCartItem>(conn, $"dbo.ShoppingCarts_removeitem @ShoppingCartId, @Quantity, @ProductId", product);
                        
                    }
                    else 
                    {
                        SqlMapper.Execute(conn, $"DELETE FROM {_shopcart} WHERE ProductId=@ProductId", new { ProductId = itemId });

                    }
                }

                var returnitem = _productRepository.Find(id);
                returnitem.Quantity = returnitem.Quantity + quantity;
                _productRepository.Update(returnitem);
                return product;

            }
        }
    }
}
