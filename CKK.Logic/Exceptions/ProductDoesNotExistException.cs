using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Exceptions
{
    public class ProductDoesNotExistException : Exception
    {
        public ProductDoesNotExistException() : base ("Product does not exist") 
        {

        }

        public ProductDoesNotExistException (string message) : base (message) 
        {

        }

        public ProductDoesNotExistException (string message, Exception inner) : base (message, inner) 
        {

        }
    }
}
