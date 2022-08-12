using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Persistance.Models
{
    [Serializable]
    public class RecordSerializable
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public RecordSerializable() : this(string.Empty, 0, 0M, 0) { }

        public RecordSerializable(string name, int id, decimal price, int qauntity) 
        {
            Name = name; 
            Id = id; 
            Price = price; 
            Quantity = qauntity; 
        }
    }
}
