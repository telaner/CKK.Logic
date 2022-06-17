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
        public readonly string FilePath = @"C:\Users\risee\Documents\School Otech\Structured project 1\CKK.Logic" + Path.DirectorySeparatorChar + "Persistance" + Path.DirectorySeparatorChar + "StoreItems.dat";

        
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
            
            using( var fsOpen = new FileStream(FilePath,FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter reader = new BinaryFormatter();
                Items = (List<StoreItem>)reader.Deserialize(fsOpen);
            }
           
        }


        public void Save()
        {
            var filename = File.Create("StoreItems.dat");
            string filepath = Path.Combine(FilePath, filename.ToString());
            using (var fsitems = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fsitems, Items);
            }
                        
        }

        public void CreatePath()
        {
            Directory.CreateDirectory(FilePath);
          
        }
    }
}
