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
        private ShoppingCartItem _product1;
        private ShoppingCartItem _product2;
        private ShoppingCartItem _product3;

        public ShoppingCart(Customer cust) 
        {
            _customer = cust;
        }

        
        public int GetCustomerID() 
        {
            return _customer.GetId();
        }

        public ShoppingCartItem GetProductById(int id)
        {
            if (_product1 != null  && _product1.GetProduct().GetId()==id)
                return _product1;
           else if (_product2 != null && _product2.GetProduct().GetId() == id)
                return _product2;
           else if (_product3 != null && _product3.GetProduct().GetId() == id)
                return _product3;
           else
                return null;
        }
        public ShoppingCartItem AddProduct(Product prod, int Quantity) 
        {
            if (Quantity < 1)
                return null;   
            
            if (_product1 != null && _product1.GetProduct().GetId()==(prod.GetId()))
            {
                _product1.SetQuantity(_product1.GetQuantity()+Quantity);
                return _product1;
            }
            
            if (_product2 != null && _product2.GetProduct().GetId() == (prod.GetId()))
            {
                _product2.SetQuantity(_product2.GetQuantity() + Quantity);
                return _product2;
            }
            
            if (_product3 != null && _product3.GetProduct().GetId() == (prod.GetId()))
            {
                _product3.SetQuantity(_product3.GetQuantity() + Quantity);
                return _product3;
            }

            if (_product1 == null)
            {
                _product1 = new ShoppingCartItem(prod, Quantity);
                return _product1;
            }

            if (_product2 == null) 
            {
                _product2 = new ShoppingCartItem(prod, Quantity);
                return _product2;
            }

            if (_product3 == null)
            {
                _product3 = new ShoppingCartItem(prod, Quantity);
                return _product3;
            }

            else
                return null;
        }
        public ShoppingCartItem AddProduct(Product prod)
        {
            return AddProduct
                (prod, 1);
        }
        public ShoppingCartItem RemoveProduct(Product prod, int Quantity) 
        {
            if (Quantity < 1)
                return null;

            else if (_product1 != null && _product1.GetProduct().GetId() == (prod.GetId()))
            {
                _product1.SetQuantity(_product1.GetQuantity() - Quantity);
                return _product1;
            }

            else if (_product2 != null && _product2.GetProduct().GetId() == (prod.GetId()))
            {
                _product2.SetQuantity(_product2.GetQuantity() - Quantity);
                return _product2;
            }

            else if (_product3 != null && _product3.GetProduct().GetId() == (prod.GetId()))
            {
                _product3.SetQuantity(_product3.GetQuantity() - Quantity);
                return _product3;
            }
            else
                return null;
        }
        public decimal GetTotal() 
        {
            decimal total = 0;
            if (_product1 != null) total += _product1.GetTotal();
            if (_product2 != null) total += _product2.GetTotal();
            if (_product3 != null) total += _product3.GetTotal();
            return total;
        }
        public ShoppingCartItem GetProduct(int prodNum)
        {
            if (prodNum == 1)
                return _product1;
            if (prodNum == 2)
                return _product2;
            if (prodNum == 3)
                return _product3;
            else
                return null;

        }

    }
}
