using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Repository.Interfaces
{
    public interface IDataUnitOfWork
    {
        public IShoppingCartItemRepository ShoppingCarts { get; }
        public IProductRepository Products { get; }
        public IOrderRepository Orders { get; }
        public ICustomerRepository Customers { get; }
    }
}
