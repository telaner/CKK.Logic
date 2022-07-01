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
using System.Collections.ObjectModel;

namespace CKK.Persistance.Models
{
    public class FileStore : IStore, ISavable, ILoadable
    {
        private List<StoreItem> Items;
        
        public readonly string FilePath = @"C:\Users\risee\Documents\School Otech\Structured project 1\CKK.Logic" + Path.DirectorySeparatorChar + "Persistance" + Path.DirectorySeparatorChar + "StoreItems.dat";

        
        private int IdCounter = 0;

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

                    newitem.Product.Id = ++IdCounter;
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
            FileStream stream = new(FilePath+"StoreItems.dat", FileMode.OpenOrCreate, FileAccess.Read);



            try

            {

                BinaryFormatter formatter = new BinaryFormatter();

                Items = (List<StoreItem>)formatter.Deserialize(stream);

                IdCounter = Items.Count + 1;

                foreach (var item in Items)

                {

                    if (item.Product.Id == 0)

                    {

                        item.Product.Id = IdCounter;

                    }

                }



            }

            catch (IOException e)

            {

                throw new IOException("There has been an error opening the file to load data", e);

            }

            catch (SerializationException ex)

            {

                Items = new();

                //throw new SerializationException("There was a problem deserializing the data: " + ex.Message, ex);

            }

            finally

            {

                stream?.Dispose();

            }

            using ( var fsOpen = new FileStream(FilePath,FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter reader = new BinaryFormatter();
                Items = (List<StoreItem>)reader.Deserialize(fsOpen);
            }
           
        }


        public void Save()
        {
            FileStream stream = new FileStream(FilePath+File.Create("StoreItems.dat"), FileMode.OpenOrCreate, FileAccess.Write);
            try

            {

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, Items);

            }
            catch (IOException e)
            {
                throw new IOException("There was a problem writing to the file", e);
            }
            catch (SerializationException ex) 
            {
                throw new SerializationException("There was a problem serializing the data: " + ex.Message, ex);
            }
            finally

            {

                stream?.Dispose();

            }
                                    
        }

        public void CreatePath()
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "Persistance");
          
        }

        public ObservableCollection<StoreItem> GetAllProductsByName(ObservableCollection<StoreItem> products, string name)
        {
            var productnames = from item in products
                               select item.Product.Name;
            string[] productarray = productnames.ToArray();
            ArrayToLower(productarray);
            string nameLower = name.ToLower();

            for (var index = 0; index < productarray.Length; ++index)
            {
                if (productarray[index] == name)
                {
                    var Findbyname = from Item in products
                                     where Item.Product.Name.ToLower().Contains(nameLower)
                                     orderby Item.Product.Name
                                     select Item;

                    var namelist = new ObservableCollection<StoreItem>(Findbyname);
                    return namelist;


                }

                else 
                {
                    var Findbyname = from Item in products
                                     where Item.Product.Name.ToLower().Contains(nameLower)
                                     orderby Item.Product.Name
                                     select Item;

                    var namelist = new ObservableCollection<StoreItem>(Findbyname);
                    return namelist;

                }
            }
            return null;

           

        }


        public ObservableCollection<StoreItem> GetAllProductsByQuantity(ObservableCollection<StoreItem> products)
        {
            var quantitySort = 
                from item in products
                orderby item.Quantity
                select item;
            var quantitySortList = new ObservableCollection<StoreItem>(quantitySort);
            return quantitySortList;
        }
        public ObservableCollection<StoreItem> GetAllProductsByPrice(ObservableCollection<StoreItem> items) 
        {
            var priceSort = 
                from item in items
                orderby item.Product.Price
                select item;
            var priceSortlist = new ObservableCollection<StoreItem>(priceSort);
            return priceSortlist;

        }
        //This should return a List<StoreItems> that are sorted by Price. (highest to lowest)

        private static void ArrayToLower(string[] data) 
        {
            for (int i = 0; i < data.Length; i++) 
            {
                data[i] = data[i].ToLower();
            }
        }
    }
}
