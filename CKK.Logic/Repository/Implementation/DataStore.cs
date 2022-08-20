using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using CKK.Logic.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Repository.Implementation
{
    public class DataStore : IStore
    {
        private readonly IProductRepository _ProductRepository;

        public DataStore(IProductRepository ProductRepository) 
        {
            
            _ProductRepository = ProductRepository;
        }

        public Product AddProduct(Product item) 
        {
            _ProductRepository.Add(item);
            return item;
        }

        public Product DeleteProduct(int id)
        {
            var item = _ProductRepository.Find(id);
            if (item != null) 
            {
                _ProductRepository.Remove(item);

                return item;
            }
            return null;
        }

        public Product UpdateProduct(Product item)
        {
            _ProductRepository.Update(item);
            return item;
        }

        public Product FindProductById(int id)
        {
            return _ProductRepository.Find(id);
        }

        public List<Product> GetAllProducts()
        {
            return _ProductRepository.GetAll().ToList();
        }

        public List<Product> GetProductsByName(string name)
        {
            return _ProductRepository.GetItemsByName(name).ToList();
        }

        public List<Product> GetProductsByPrice(decimal price)  
        {
            return _ProductRepository.GetItemsByPrice(price).ToList();
        }

        public List<Product> GetProductsByQuantity(int quantity)
        {
            return _ProductRepository.GetItemsByQuantity(quantity).ToList();
        }

               
    }
}
