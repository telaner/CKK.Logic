using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Interfaces
{
    public abstract class Entity
    {
        public int Id;
        public string Name;
        public Entity(int id, string name) 
        { 
            Id = id; 
            Name = name; 
        }
        
    }
}
