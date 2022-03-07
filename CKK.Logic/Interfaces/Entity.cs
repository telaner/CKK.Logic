using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Interfaces
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public string Name { get; }
        public Entity(int id, string name) 
        {
            Name = name;
            Id = id;
        }
    }
}
