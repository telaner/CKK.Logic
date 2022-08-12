using CKK.Logic.Models;
using CKK.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Net;


namespace CKK.MockClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Socket sck = null;

        private EndPoint epLocal;

        private EndPoint epRemote;

        private byte[] buffer;

        public FileStore fileStore;

        public ObservableCollection<StoreItem> _Items { get; private set; }

        public ObservableCollection<ShoppingCartItem> _ShoppingCartItems { get; set; }

        public ShoppingCart shopcart;

        private int custidcounter = 0;
        private int shopcartid = 0;

        private Customer customer;

        public MainWindow()
        {
            FileStore fileStore = (FileStore)Application.Current.FindResource("globStore");

            customer = new Customer();

            shopcart = new ShoppingCart(customer);

            _Items = new ObservableCollection<StoreItem>();


            

            fileStore.Load();

            foreach (StoreItem item in fileStore.Items)
            {
                _Items.Add(item);
            }


            _ShoppingCartItems = new ObservableCollection<ShoppingCartItem>(shopcart.ShoppingCartItems);


            InitializeComponent();

            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            epLocal = new IPEndPoint(IPAddress.Parse(GetLocalIP()), 11001);
            sck.Bind(epLocal);
            buffer = new byte[4096];

            epRemote = new IPEndPoint(IPAddress.Parse(GetLocalIP()), 11000);
            sck.Connect(epRemote);
            sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);

            custidcounter++;
            shopcartid++;

            custIdBox.Text = custidcounter.ToString();
            cartIdBox.Text = shopcartid.ToString();

            StoreInventory.ItemsSource = _Items;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            FileStore fileStore = (FileStore)Application.Current.FindResource("globStore");

            StoreItem purchaseitem = (StoreItem)StoreInventory.SelectedItem;


            shopcart.AddProduct(purchaseitem.Product, int.Parse(quantityBox.Text));

            _ShoppingCartItems = new ObservableCollection<ShoppingCartItem>(shopcart.ShoppingCartItems);

            ShoppingCart.ItemsSource = _ShoppingCartItems;


            _Items.Remove(purchaseitem);

            purchaseitem.Quantity = purchaseitem.Quantity - int.Parse(quantityBox.Text);

            _Items.Add(purchaseitem);

            quantityBox.Clear();



        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            FileStore fileStore = (FileStore)Application.Current.FindResource("globStore");

            ShoppingCartItem returnitem = (ShoppingCartItem)ShoppingCart.SelectedItem;


            if (returnitem.Quantity - int.Parse(quantityBox.Text) <= 0)
            {
                shopcart.RemoveProduct(returnitem.Product.Id, int.Parse(quantityBox.Text));
                _ShoppingCartItems.Remove(returnitem);
            }
            else
            {
                shopcart.RemoveProduct(returnitem.Product.Id, int.Parse(quantityBox.Text));

                _ShoppingCartItems.Remove(returnitem);


                _ShoppingCartItems.Add(returnitem);
            }

            ShoppingCart.ItemsSource = _ShoppingCartItems;

            var products =
                from product in _Items
                select product.Product;

            if (products.Contains(returnitem.Product))
            {
                fileStore.AddStoreItem(returnitem.Product, int.Parse(quantityBox.Text));
                _Items.Clear();

                foreach (StoreItem item in fileStore.Items)
                {
                    _Items.Add(item);
                }
            }


            quantityBox.Clear();


        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] mybuffer = new byte[8192];

            try
            {
                shopcart.CustomerId = 23;
                Customer cust = new Customer();
                cust.CustomerId = 1;
                cust.Id = 2;
                cust.Name = "jim";
                shopcart.Customer = cust;
                shopcart.ShoppingCartId = 34;

                byte[] msg = JsonSerializer.SerializeToUtf8Bytes<object>(shopcart);
                int bytesSent = sck.Send(msg);

                
            }

            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

            


        }

        private static string GetLocalIP()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }

        private void MessageCallBack(IAsyncResult aResult)
        {
            var utf8Reader = new Utf8JsonReader(buffer);
            try
            {

                int size = sck.EndReceiveFrom(aResult, ref epRemote);

                if (size > 0)
                {
                    byte[] receivedData = new byte[1500];

                    receivedData = (byte[])aResult.AsyncState;
                    ASCIIEncoding eEncoding = new ASCIIEncoding();
                    string receivedMessage = eEncoding.GetString(receivedData);

                }

                buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
                
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
            }
        }
    }
}
        
