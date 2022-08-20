using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Exceptions;

namespace CKK.Logic.Models
{
    public class Store : Interfaces.Entity
    {

        private List<StoreItem> Items;
        private ObservableCollection<StoreItem> namelist;

        private int StoreIdcounter = 0;

        public Store() : base()

        {

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
                if (newitem.Product.Id <=0 )
                {

                    newitem.Product.Id = ++StoreIdcounter;
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
                return  null;
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
        public ObservableCollection<StoreItem> GetAllProductsByName(ObservableCollection<StoreItem> products, string name)
        {
            var productnames = from item in products
                               select item.Product.Name;
            string[] productarray = productnames.ToArray();

            Array.Sort(productarray);
            ArrayToLower(productarray);

            string nameLower = name.ToLower();


            int firstindex = 0;
            int lastindex = productarray.Length - 1;


            while (firstindex <= lastindex)
            {
                int middle = (firstindex + lastindex) / 2;
                int result = nameLower.CompareTo(productarray[middle]);


                if (result == 0)
                {


                    var Findbyname = from Item in products
                                     where Item.Product.Name.Contains(nameLower)
                                     select Item;

                    var namelist = new ObservableCollection<StoreItem>(Findbyname);
                    return namelist;

                }
                else if (result > 0)
                {
                    firstindex = middle + 1;
                }
                else
                {
                    lastindex = middle - 1;
                }


            }

            {
                var Findbyname = from Item in products
                                 where Item.Product.Name.Contains(nameLower)
                                 select Item;

                var namelist = new ObservableCollection<StoreItem>(Findbyname);
                return namelist;

            }
        }





        public ObservableCollection<StoreItem> GetAllProductsByQuantity(ObservableCollection<StoreItem> products)
        {
            var productsarray = products.ToArray();

            SortedArray(productsarray, 0, productsarray.Length - 1);
            var sortproductslist = productsarray.ToList();
            sortproductslist = new List<StoreItem>();
            ObservableCollection<StoreItem> sortproducts = new ObservableCollection<StoreItem>(sortproductslist);
            return sortproducts;



        }

        private static void SortedArray(StoreItem[] items, int low, int high)
        {
            if ((high - low) >= 1)
            {
                int middle1 = (low + high) / 2;
                int middle2 = middle1 + 1;

                SortedArray(items, low, middle1);
                SortedArray(items, middle2, high);

                Merge(items, low, middle1, middle2, high);
            }
        }

        private static void Merge(StoreItem[] items, int left, int middle1, int middle2, int right)
        {
            var quantity = from item in items
                           select item.Quantity;

            int[] quantityarray = quantity.ToArray();

            int leftIndex = left;
            int rightIndex = middle2;
            int combinedIndex = left;
            int[] combined = new int[quantityarray.Length];

            while (leftIndex <= middle1 && rightIndex <= right)
            {
                if (quantityarray[leftIndex] <= quantityarray[rightIndex])
                {
                    combined[combinedIndex++] = quantityarray[leftIndex++];
                }
                else
                {
                    combined[combinedIndex++] = quantityarray[rightIndex++];
                }
            }

            if (leftIndex == middle2)
            {
                while (rightIndex <= right)
                {
                    combined[combinedIndex++] = quantityarray[rightIndex++];
                }
            }
            else
            {
                while (leftIndex <= middle1)
                {
                    combined[combinedIndex++] = quantityarray[leftIndex++];
                }
            }
            for (int i = left; i <= right; ++i)
            {
                quantityarray[i] = combined[i];
            }
        }

        //This should return a List<StoreItems> that are sorted by quantity. (highest to lowest)

        public ObservableCollection<StoreItem> GetAllProductsByPrice(ObservableCollection<StoreItem> items)
        {
            var priceSort =
                from item in items
                orderby item.Product.Price
                select item;
            return (ObservableCollection<StoreItem>)priceSort;

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
