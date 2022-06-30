using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Caliburn.Micro;
using CKK.Logic.Interfaces;
using System.Collections.ObjectModel;
using CKK.Persistance.Models;
using System.IO;

namespace CKK.UI.Views
{
    /// <summary>
    /// Interaction logic for StoreInventorypage.xaml
    /// </summary>
    public partial class StoreInventorypage : Window
    {
        public FileStore _Store;

        public ObservableCollection<StoreItem> _Items { get; private set; }
        public ObservableCollection<StoreItem> Searchlist { get; private set; }
        public ObservableCollection<StoreItem> QuantityList { get; private set; }
        public ObservableCollection<StoreItem> Pricelist { get; private set; }

        public int StoreIdcounter = 0;



        Product pearlnecklace = new Product { Name = "Pearl Necklace", Id = 123, Price = 65.00m };
        Product goldring = new Product { Name = "Gold Ring", Id = 321, Price = 200.00m };
        Product pearlearrings = new Product { Name = "Pearl Earrings", Id = 122, Price = 50.00m };
        Product diamondearrings = new Product { Name = "Diamond Earrings", Id = 322, Price = 65.00m };
        

        public StoreInventorypage(FileStore store)
        {
            _Store = store;
            InitializeComponent();
            
            _Items = new ObservableCollection<StoreItem>();

            _Items.Add(new StoreItem(pearlnecklace, 6));
            _Items.Add(new StoreItem(goldring, 3));
            _Items.Add(new StoreItem(pearlearrings, 5));
            _Items.Add(new StoreItem(diamondearrings, 4));

            StoreInventory.ItemsSource = _Items;
            
            


        }
       
       

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            
            
            Product newprod = new Product { Name = newNamebox.Text, Id = int.Parse(newIdbox.Text), Price=decimal.Parse(newPricebox.Text)};
            _Items.Add(new StoreItem(newprod, int.Parse(newQuantitybox.Text)));

            _Store.AddStoreItem(newprod, int.Parse(newQuantitybox.Text));

            _Store.Save();


            newNamebox.Clear();
            newIdbox.Clear();
            newPricebox.Clear();
            newQuantitybox.Clear();

            

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            _Items.Remove((StoreItem)StoreInventory.SelectedItem);
            
            _Store.Save();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {

            StoreItem edititem = (StoreItem)StoreInventory.SelectedItem;
            editNamebox.Text = edititem.Product.Name;
            editIdbox.Text = edititem.Product.Id.ToString();
            editPricebox.Text = edititem.Product.Price.ToString();
            editQuantitybox.Text = edititem.Quantity.ToString();

            _Items.Remove((StoreItem)StoreInventory.SelectedItem);



        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Product newprod = new Product { Name = editNamebox.Text, Id = int.Parse(editIdbox.Text), Price = decimal.Parse(editPricebox.Text) };
            _Items.Add(new StoreItem(newprod, int.Parse(editQuantitybox.Text)));
            editNamebox.Clear();
            editIdbox.Clear();
            editPricebox.Clear();
            editQuantitybox.Clear();

            _Store.Save();
        }

       

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
           
            string searchkey = searchbox.Text;
            Searchlist = _Store.GetAllProductsByName(_Items, searchkey);
            SearchResults.ItemsSource = Searchlist;

        }

        private void quantitySortButton_Click(object sender, RoutedEventArgs e)
        {
            StoreInventory.ItemsSource = "";
            _Store.GetAllProductsByQuantity(_Items);
            QuantityList = _Store.GetAllProductsByQuantity(_Items);
            _Items = QuantityList;
            StoreInventory.ItemsSource = _Items;
           

        }

        private void priceSortButton_Click(object sender, RoutedEventArgs e)
        {
            StoreInventory.ItemsSource = "";
            _Store.GetAllProductsByPrice(_Items);
            Pricelist = _Store.GetAllProductsByPrice(_Items);
            _Items = Pricelist;
            StoreInventory.ItemsSource = _Items;
            

        }
    }
}
