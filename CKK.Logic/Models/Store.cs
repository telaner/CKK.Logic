using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class Store
    {
        private int _id;
        private string _name;
        private Product _product1;
        private Product _product2;
        private Product _product3;

        public int GetID() 
        {
            return _id;
        }
        public void SetID(int id) 
        {
            _id = id; 
        }
        public string GetName() 
        {
            return _name;
        }
        public void SetName(string name) 
        {
            _name = name;
        }
        public void AddStoreItem(Product prod)
        {
        if (_product1 == null) 
            {
                prod = _product1;
                if (_product1 != null)
                {
                    prod = _product2;
                    if (_product2 != null) 
                    {
                        prod = _product3;
                    }
                }
            }
        }
        public void RemoveStoreItem(int productNumber) 
        {
            if (_product1.Equals(productNumber))
            {
                _product1 = null;
            }
            if (_product2.Equals(productNumber)) 
            {
                _product2 = null;
            }
            if (_product3.Equals(productNumber)) 
            {
                _product3 = null;
            }
           
        }
        public Product GetStoreItem(int productNumber) 
        {
            if (_product1.Equals(productNumber)) 
            {
                return _product1; 
            }
            if (_product2.Equals(productNumber)) 
            { 
                return _product2; 
            }
            if (_product3.Equals(productNumber))
            {
                return _product3;
            }
            else 
            { 
                return null; 
            }
        }
        public Product FindStoreItemById(int id) 
        {
            if (_product1.Equals(id)) 
            {
                return _product1;
            }
            if (_product1.Equals(id)) 
            {
                return _product2; 
            }
            if (_product3.Equals(_id)) 
            { 
                return _product3; 
            }
            else 
            { 
                return null; 
            }
        }
    }
}
