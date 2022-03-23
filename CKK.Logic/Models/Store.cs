using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Exceptions;

namespace CKK.Logic.Models
{
    public class Store : Interfaces.Entity, Interfaces.IStore
    {

        private List<StoreItem> Items;

        public Store() : base()

        {

            Items = new List<StoreItem>();

        }


        public StoreItem AddStoreItem(Product prod, int quantity)
        {

            if (quantity <= 0)
            {
                return null;
                throw new InventoryItemStockTooLowException();
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
                existingItem.Quantity += quantity;
                return existingItem;
            }
            else
                return null;
        }


        public StoreItem RemoveStoreItem(int id, int quantity)
        {
            if (quantity < 0) 
            {
                throw new ArgumentOutOfRangeException(nameof(quantity),"Quantity must be 0 or greater.");
            }
            var existingItem = FindStoreItemById(id);
            if (Items.Contains(existingItem) && ((existingItem.Quantity - quantity) >= 0))
            {
                existingItem.Quantity -= quantity;
                return existingItem;
            }
            if (Items.Contains(existingItem) && ((existingItem.Quantity - quantity) <= 0))
            {
                existingItem.Quantity = 0;
                return existingItem;
            }
            if (!Items.Contains(existingItem))
            {
                throw new ProductDoesNotExistException();
            }
            else
            {
                return null;
            }
        }


        public List<StoreItem> GetStoreItems()
        {

            return Items;

        }

        public StoreItem FindStoreItemById(int id)
        {
            if (id < 0) 
            {
                throw new InvalidIdException();
            }
            var FindbyId = from Item in Items
                           where Item.Product.Id == id
                           select Item;
            return FindbyId.FirstOrDefault();

        }
    }
} 
