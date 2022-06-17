using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using CKK.Persistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using CKK.Logic.Exceptions;

namespace CKK.Persistance.Models
{
    public class FileStore : IStore, ISavable, ILoadable
    {
        private List<StoreItem> Items;
        public string FilePath = @"C:\Users\risee\Documents\" + Path.DirectorySeparatorChar + "Persistance" + Path.DirectorySeparatorChar + "StoreItems.dat";
        private int Idcounter = 0;

        static void Main() { }

        public FileStore() 
        {
            CreatePath();
            Items = new List<StoreItem>();
        }
        public StoreItem AddStoreItem(Product prod, int quantity)
        {

            if (quantity <= 0)
            {

                throw new InventoryItemStockTooLowException();
            }
            var existingItem = FindStoreItemById(prod.Id);
            if (existingItem == null)
            {

                var newitem = new StoreItem(prod, quantity);
                if (newitem.Product.Id <= 0)
                {

                    newitem.Product.Id = ++Idcounter;
                }


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
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be 0 or greater.");
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

        public StoreItem DeleteStoreItem(int id)
        {
            var existingItem = FindStoreItemById(id);
            if (Items.Contains(existingItem))
            {
                Items.Remove(existingItem);
                return null;
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

        public void Load()
        {
            FileStream fs = new FileStream(FilePath, FileMode.Open);
            BinaryFormatter reader = new BinaryFormatter();
            StoreItem Items = (StoreItem)reader.Deserialize(fs);
        }


        public void Save()
        {
            
            FileStream fs = new FileStream(FilePath, FileMode.Append);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, Items);
                        
        }

        public void CreatePath()
        {
            Directory.CreateDirectory(FilePath);
          
        }
    }
}
