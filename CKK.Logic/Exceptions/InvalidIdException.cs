using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Exceptions
{
    public class InvalidIdException : Exception
    {
        public InvalidIdException() : base("Id cannot be less than 0") 
        {

        }   
        
        public InvalidIdException(string message) : base(message) 
        {

        }

        public InvalidIdException(string message, Exception inner) : base(message, inner) 
        {

        }
    }
}
