using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Repository.Interfaces
{

    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        void Add(T item);
        void Update(T item);
        void Remove(T item);
    }
}
