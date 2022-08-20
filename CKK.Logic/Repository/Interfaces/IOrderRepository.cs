using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Repository.Interfaces
{
    public interface IOrderRepository : IRepository<OrderSummary>
    {
        Order GetOrderByCustomer(int Id);
    }
}
