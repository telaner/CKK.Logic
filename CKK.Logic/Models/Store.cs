using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class Store
    {
        private int _id;
        private string _name;
        private List<StoreItem> Items;

        public Store()

        {
            Items = new List<StoreItem>();
        }

        public int GetId()
        {
            return _id;
        }
        public void SetId(int id)
        {
            _id = id;
        }
        public string GetName()
        {
            return _name;
        }
        public void SetName(string name)
        {
            _name = name;
        }
        public StoreItem AddStoreItem(Product prod, int quantity)
        {
            
            if (quantity <= 0)
            {
                return null;
            }
            var existingItem = FindStoreItemById(prod.GetId());
            if (existingItem == null)
            {
                Items.Add(new StoreItem(prod, quantity));
                return new StoreItem(prod, quantity);
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
                where Item.GetProduct().GetId() == id
                select Item;
                return FindbyId.FirstOrDefault();
                
            }
        }
    } 
