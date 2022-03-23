using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Exceptions;

namespace CKK.Logic.Interfaces
{
    public abstract class Entity
    {
        public int Id;
        
        public int SetId()
        {
            if (Id < 0)
            {
                throw new InvalidIdException();
            }
                
            else 
            {
                return Id;
            }

        }
        public string Name { get; set; }
        public Entity() { }
        
    }
}
