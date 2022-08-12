using System.Net.Sockets;
using System.Net;
using CKK.Logic.Models;
using CKK.Persistance.Models;
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

    private ShoppingCart cart;

    private FileStore myStore;

    private Queue<ShoppingCart> shops = new Queue<ShoppingCart>();


    static void Main(string[] args)
    {
        Server server = new Server();
        server.sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        server.sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

        Console.Write("Local Port No.: ");
        server.epLocal = new IPEndPoint(IPAddress.Parse(GetLocalIP()), 11000);
        server.sck.Bind(server.epLocal);




        Console.Write("\nRemote Port No.:");
        server.epRemote = new IPEndPoint(IPAddress.Parse(GetLocalIP()), 11001);
        server.sck.Connect(server.epRemote);


        server.buffer = new byte[1500];
        server.sck.BeginReceiveFrom(server.buffer, 0, server.buffer.Length, SocketFlags.None, ref server.epRemote, new AsyncCallback(server.MessageCallBack), server.buffer);

        System.Threading.Thread.Sleep(5000);

        Console.WriteLine("\nConnected Successfully");




        while (Console.ReadLine() != "done") ;

        server.sck.Shutdown(SocketShutdown.Both);
        server.sck.Close();
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
                    cart = json.ToObject<ShoppingCart>();
                    msg = Encoding.Default.GetBytes($"Successfully added order to the Queue. There are :'{cart.ShoppingCartItems.Count}' orders ahead of you.");

                    Console.WriteLine("Received Data");

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
            //SendTextMessage();
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp.ToString());
        }

    }

    private void SendTextMessage()
    {
        ASCIIEncoding enc = new ASCIIEncoding();
        byte[] msg = new byte[1500];
        Console.WriteLine("Please enter message");
        msg = enc.GetBytes(Console.ReadLine());
        sck.Send(msg);
    }



}
