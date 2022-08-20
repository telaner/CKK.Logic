using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Product Find(int id);
        
        IEnumerable<Product> GetItemsByName(string name);
        IEnumerable<Product> GetItemsByQuantity(int quantity);
        IEnumerable<Product> GetItemsByPrice(decimal price);
    }
}
