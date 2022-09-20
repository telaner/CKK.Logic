using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CKK.Logic.Repository.Interfaces;
using CKK.Logic.Models;
using CKK.Logic.Repository.Implementation;

namespace CKK.Online.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _products;
        private readonly IShoppingCartItemRepository _shopitems;
        private readonly IOrderRepository _order;


        public ProductController(IProductRepository products, IShoppingCartItemRepository items, IOrderRepository order)
        {
            _products = products;
            _shopitems = items;
            _order = order;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            var productList = _products.GetAll().ToList();
            return View(productList);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _products.Find(id ?? 0);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(IFormCollection collection)
        {
            
            {
                var id = Convert.ToInt32(collection["id"]);
                var quantity = Convert.ToInt32(collection["cartCount"]);
                var shoppingCartItem = new ShoppingCartItem
                {
                    ProductId = id,
                    Quantity = quantity
                };
                _shopitems.AddtoCart(id,quantity);
                
                return RedirectToAction(nameof(Cart));
            }
            
        }
        public ActionResult Cart()
        {

            var shoppingCartId = 1;
            var cartList = _shopitems.GetProducts(shoppingCartId).ToList();
            foreach (var product in cartList)
            {
                product.Product = _products.Find(product.ProductId);
            }
            return View(cartList);

        }

        public ActionResult Order(int cartId) 
        {
            var carlist = _shopitems.GetProducts(1).ToList();

            Order order = new Order
            {
                OrderID = 1,
                ShoppingCartId = 1
            };
            return RedirectToAction("OrderComplete");

        }
       
        public ActionResult OrderComplete(string message)
        {
            return View((object)message);
        }


    }
}
