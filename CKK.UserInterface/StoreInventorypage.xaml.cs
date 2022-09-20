using CKK.Logic.Models;
using CKK.Logic.Repository.Implementation;
using CKK.Logic.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CKK.UserInterface
{
    /// <summary>
    /// Interaction logic for StoreInventorypage.xaml
    /// </summary>
    public partial class StoreInventorypage : Window
    {


        private static readonly IConnectionFactory factory = new DatabaseConnectionFactory();

        public ProductRepository _Store = new ProductRepository(factory); 
        

        public ObservableCollection<Product> _Items { get; private set; }
        public ObservableCollection<Product> Searchlist { get; private set; }
        public ObservableCollection<Product> QuantityList { get; private set; }
        public ObservableCollection<Product> Pricelist { get; private set; }

        public int StoreIdcounter = 0;



       

        public StoreInventorypage()
        {

            InitializeComponent();


            UpdateBinding();

        }

        private void UpdateBinding() 
        {
            var items = _Store.GetAll().ToList();

            _Items = new ObservableCollection<Product>(items);

            StoreInventory.ItemsSource = _Items;
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {

            Product newprod = new Product { Name = newNamebox.Text, Price = decimal.Parse(newPricebox.Text), Quantity = int.Parse(newQuantitybox.Text) };


            _Store.Add(newprod);


            
            UpdateBinding();

            newNamebox.Clear();
            newIdbox.Clear();
            newPricebox.Clear();
            newQuantitybox.Clear();

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = (Product)StoreInventory.SelectedItem;
            _Store.Remove(product);

            UpdateBinding();
            
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {

            Product edititem = (Product)StoreInventory.SelectedItem;
            editNamebox.Text = edititem.Name;
            editIdbox.Text = edititem.Id.ToString();
            editPricebox.Text = edititem.Price.ToString();
            editQuantitybox.Text = edititem.Quantity.ToString();

            _Store.Remove(edititem);

            UpdateBinding();

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Product newprod = new Product { Name = editNamebox.Text, Id = int.Parse(editIdbox.Text), Price = decimal.Parse(editPricebox.Text), Quantity = int.Parse(editQuantitybox.Text) };
            _Items.Add(newprod);
            editNamebox.Clear();
            editIdbox.Clear();
            editPricebox.Clear();
            editQuantitybox.Clear();


        }



        private void searchButton_Click(object sender, RoutedEventArgs e)
        {

            string searchkey = searchbox.Text;
            var searchList = _Store.GetItemsByName(searchkey);
            Searchlist = new ObservableCollection<Product>(searchList);
            SearchResults.ItemsSource = Searchlist;

        }

        private void quantitySortButton_Click(object sender, RoutedEventArgs e)
        {
            //StoreInventory.ItemsSource = "";
            //_Store.GetAllProductsByQuantity(_Items);
            //QuantityList = _Store.GetAllProductsByQuantity(_Items);
            //_Items = QuantityList;
            //StoreInventory.ItemsSource = _Items;


        }

        private void priceSortButton_Click(object sender, RoutedEventArgs e)
        {
            //StoreInventory.ItemsSource = "";
            //_Store.GetAllProductsByPrice(_Items);
            //Pricelist = _Store.GetAllProductsByPrice(_Items);
            //_Items = Pricelist;
            //StoreInventory.ItemsSource = _Items;


        }
    }
}
