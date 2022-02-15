using Microsoft.VisualStudio.TestTools.UnitTesting;
using CKK.Logic.Models;


namespace CKK.tests
{
    [TestClass]
    public class CKKTests
    {
        [TestMethod]
        public void AddingProduct()
        {
            Customer customer = new Customer();
            customer.SetName("John Doe");
            customer.SetId(456);
            customer.SetAddress("123 Main Street");

            Product _product1 = new Product();
            _product1.SetName("Toy Boat");
            _product1.SetId(123);
            _product1.SetPrice(3);
            int product1quantity = 2;

            Product _product2 = new Product();
            _product2.SetName("Necklace");
            _product2.SetId(213);
            _product2.SetPrice(6);
            int product2quantity = 3;

            Product _product3 = new Product();
            _product3.SetName("Hat");
            _product3.SetId(312);
            _product3.SetPrice(4);
            int product3quantity = 2;


            ShoppingCart shoppingCart = new(customer);
            shoppingCart.GetProductById(_product1.GetId());
            shoppingCart.GetProductById(_product2.GetId());
            shoppingCart.GetProductById(_product3.GetId());

            var actual = shoppingCart.AddProduct(_product1, product1quantity);
            actual = shoppingCart.AddProduct(_product1, 1);

            Assert.AreEqual(3, actual.GetQuantity());

        }

        [TestMethod]
        public void RemovingProduct()
        {
            Customer customer = new Customer();
            customer.SetName("John Doe");
            customer.SetId(456);
            customer.SetAddress("123 Main Street");

            Product _product1 = new Product();
            _product1.SetName("Toy Boat");
            _product1.SetId(123);
            _product1.SetPrice(3);
            int product1quantity = 2;

            Product _product2 = new Product();
            _product2.SetName("Necklace");
            _product2.SetId(213);
            _product2.SetPrice(6);
            int product2quantity = 3;

            Product _product3 = new Product();
            _product3.SetName("Hat");
            _product3.SetId(312);
            _product3.SetPrice(4);
            int product3quantity = 2;


            ShoppingCart shoppingCart = new(customer);
            shoppingCart.GetProductById(123);
            shoppingCart.GetProductById(213);
            shoppingCart.GetProductById(312);

            shoppingCart.AddProduct(_product2, product2quantity);
            var actual = shoppingCart.RemoveProduct(_product2.GetId(), 1);

            Assert.AreEqual(2, actual.GetQuantity());

        }

        [TestMethod]
        public void GettingTotal()
        {
            Customer customer = new Customer();
            customer.SetName("John Doe");
            customer.SetId(456);
            customer.SetAddress("123 Main Street");

            Product _product1 = new Product();
            _product1.SetName("Toy Boat");
            _product1.SetId(123);
            _product1.SetPrice(3);
            int product1quantity = 2;

            Product _product2 = new Product();
            _product2.SetName("Necklace");
            _product2.SetId(213);
            _product2.SetPrice(6);
            int product2quantity = 3;

            Product _product3 = new Product();
            _product3.SetName("Hat");
            _product3.SetId(312);
            _product3.SetPrice(4);
            int product3quantity = 2;
            
            ShoppingCart shoppingCart = new(customer);
           
            shoppingCart.AddProduct(_product1, product1quantity);
            
            shoppingCart.AddProduct(_product2, product2quantity);

            shoppingCart.AddProduct(_product3, product3quantity);


            var actual = shoppingCart.GetTotal();

            Assert.AreEqual(32, actual);
            
        }
    }
    }
