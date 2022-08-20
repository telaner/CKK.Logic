using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Models;

namespace CKK.Logic.Interfaces
{
    public interface IStore
    {
        Product AddProduct(Product item);
        Product UpdateProduct(Product item);
        List<Product> GetAllProducts();
        Product FindProductById(int id);

        public Product DeleteProduct(int id);

        public List<Product> GetProductsByName(string name);
        public List<Product> GetProductsByQuantity(int quantity);
        public List<Product> GetProductsByPrice(decimal price);
    }
}
