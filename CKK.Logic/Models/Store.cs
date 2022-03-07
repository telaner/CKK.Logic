using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class Store : Interfaces.Entity
    {
        
        private List<StoreItem> Items;

        public Store(int id, string name) : base(id, name)

        {

            Items = new List<StoreItem>();

        }

      
        public StoreItem AddStoreItem(Product prod, int quantity)
        {
            
            if (quantity <= 0)
            {
                return null;
            }
            var existingItem = FindStoreItemById(prod.Id);
            if (existingItem == null)
            {
                var newitem = new StoreItem(prod, quantity);
                Items.Add(newitem);
                return newitem;
                
            }

            if (Items.Contains(existingItem))
            {
                existingItem.SetQuantity(existingItem.GetQuantity() + quantity);
                return existingItem;
            }
            else
                return null;
        }


        public StoreItem RemoveStoreItem(int id, int quantity)
        {
            var existingItem = FindStoreItemById(id);
            if (Items.Contains(existingItem) && ((existingItem.GetQuantity() - quantity) >= 0))
            {
                existingItem.SetQuantity((existingItem.GetQuantity()) - quantity);
                return existingItem;
            }
            if (Items.Contains(existingItem) && ((existingItem.GetQuantity() - quantity) <= 0))
            {
                existingItem.SetQuantity(0);
                return existingItem;
            }
            else
                return null;
        }

        
            public List<StoreItem> GetStoreItems()
            {         
            
            return Items;
                
            }
            public StoreItem FindStoreItemById(int id)
            {
                var FindbyId = from Item in Items
                where Item.GetProduct().Id == id
                select Item;
                return FindbyId.FirstOrDefault();
                
            }
        }
    } 
