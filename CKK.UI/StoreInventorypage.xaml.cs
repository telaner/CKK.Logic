﻿using CKK.Logic.Models;
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
using CKK.Logic.Repository.Implementation;

namespace CKK.UI.Views
{
    /// <summary>
    /// Interaction logic for StoreInventorypage.xaml
    /// </summary>
    public partial class StoreInventorypage : Window
    {
        public DataStore _Store;

        public ObservableCollection<Product> _Items { get; private set; }
        public ObservableCollection<Product> Searchlist { get; private set; }
        public ObservableCollection<Product> QuantityList { get; private set; }
        public ObservableCollection<Product> Pricelist { get; private set; }

        public int StoreIdcounter = 0;



        Product pearlnecklace = new Product { Name = "Pearl Necklace", Id = 123, Price = 65.00m, Quantity = 6 };
        Product goldring = new Product{ Name = "Gold Ring", Id = 321, Price = 200.00m, Quantity = 3};
        Product pearlearrings = new Product{ Name = "Pearl Earrings", Id = 122, Price = 50.00m, Quantity = 5};
        Product diamondearrings = new Product{ Name = "Diamond Earrings", Id = 322, Price = 65.00m, Quantity = 4};
        

        public StoreInventorypage()
        {
            
            DatabaseConnectionFactory db = new DatabaseConnectionFactory();
            

            InitializeComponent();

            _Store.AddProduct(pearlnecklace);
            _Store.AddProduct(goldring);
            _Store.AddProduct(pearlearrings);
            _Store.AddProduct(diamondearrings);

            _Items = new ObservableCollection<Product>((IEnumerable<Product>)_Store.GetAllProducts());


            StoreInventory.ItemsSource = _Items;

        }



       
       

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            
            
            Product newprod = new Product { Name = newNamebox.Text, Id = int.Parse(newIdbox.Text), Price=decimal.Parse(newPricebox.Text), Quantity = int.Parse(newQuantitybox.Text) };
            

            _Store.AddProduct(newprod);
            
            //_Store.Items = new List<StoreItem>(_Items);
            //_Store.Save();


            newNamebox.Clear();
            newIdbox.Clear();
            newPricebox.Clear();
            newQuantitybox.Clear();

            

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = (Product)StoreInventory.SelectedItem;
            _Store.DeleteProduct(product.Id);
            
            //_Store.Save();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {

            Product edititem = (Product)StoreInventory.SelectedItem;
            editNamebox.Text = edititem.Name;
            editIdbox.Text = edititem.Id.ToString();
            editPricebox.Text = edititem.Price.ToString();
            editQuantitybox.Text = edititem.Quantity.ToString();

            _Store.DeleteProduct(edititem.Id);

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
            var searchList = _Store.GetProductsByName(searchkey);
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
