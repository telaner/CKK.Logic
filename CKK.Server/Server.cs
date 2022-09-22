using System.Net.Sockets;
using System.Net;
using CKK.Logic.Models;
using System.Text;
using System.Text.Json;
using System;

static class JsonExtension
{
    public static T ToObject<T>(this JsonElement element)
    {
        var json = element.GetRawText();
        return JsonSerializer.Deserialize<T>(json);
    }
}
public class Server
{

    private Socket sck = null;

    private EndPoint epLocal;

    private EndPoint epRemote;

    private byte[] buffer;

    private Order cart;

    private Queue<Order> shops = new Queue<Order>();


    static void Main(string[] args)
    {
        Server server = new Server();

        server.Startup();

        while (true) 
        {
        }
            
        

    }

    public void Startup() 
    {
        
        sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        try
        {
            
            epLocal = new IPEndPoint(IPAddress.Parse(GetLocalIP()), 11000);
            sck.Bind(epLocal);

            
            epRemote = new IPEndPoint(IPAddress.Parse(GetLocalIP()), 11001);
            sck.Connect(epRemote);

            buffer = new byte[1500];
            sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);

            System.Threading.Thread.Sleep(3700);

            Console.WriteLine("\nConnected Successfully");

        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.ToString());
        }
        

    }


    static string GetLocalIP()
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
        byte[] msg = null;
        try
        {
            int size = sck.EndReceiveFrom(aResult, ref epRemote);

            if (size > 0)
            {
                byte[] receivedData = new byte[1500];

                receivedData = (byte[])aResult.AsyncState;

                try
                {
                    var utf8Reader = new Utf8JsonReader(receivedData);
                    var json = (JsonElement)JsonSerializer.Deserialize<object>(ref utf8Reader);
                    cart = json.ToObject<Order>();
                    shops.Enqueue(cart);
                    msg = Encoding.Default.GetBytes($"Successfully added order to the Queue. There are :'{shops.Count}' orders ahead of you.");

                    SendMessage("Order Received");

                }

                catch
                {
                    Console.WriteLine("Message Received but could not be read.");
                    msg = Encoding.Default.GetBytes("Object Failed to be processed.");
                }
            }



            buffer = new byte[1500];
            sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
            sck.Send(msg);
            
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp.ToString());
        }

    }

    private void SendMessage(string message)
    {
        ASCIIEncoding enc = new ASCIIEncoding();
        byte[] msg = new byte[1500];
        
        msg = enc.GetBytes(Console.ReadLine());
        sck.Send(msg);
        DateTime localDate = DateTime.Now;
    }



}
