using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Models;

namespace CKK.Logic.Interfaces
{
    public interface IStore
    {
        StoreItem AddStoreItem(Product prod, int quantity);

        StoreItem RemoveStoreItem(int id, int quantity);

        List<StoreItem> GetStoreItems();

        StoreItem FindStoreItemById(int id);

        StoreItem DeleteStoreItem(int id);

        ObservableCollection<StoreItem> GetAllProductsByName(ObservableCollection<StoreItem> products, string name);

        //This function should return a List<StoreItem> that has all of the items that match the search. You
        //can choose if you would like it to be any letter(s) that match, or if it is just the beginning that you
        //compare. Your results don't necessarily have to be in any order, but it could be alphabetical.

        ObservableCollection<StoreItem> GetAllProductsByQuantity(ObservableCollection<StoreItem> items);

        //This should return a List<StoreItems> that are sorted by quantity. (highest to lowest)

        ObservableCollection<StoreItem> GetAllProductsByPrice(ObservableCollection<StoreItem> items);
            //This should return a List<StoreItems> that are sorted by Price. (highest to lowest)
    }
}
