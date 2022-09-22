using CKK.Logic.Models;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;


public class Client
{
    private Socket sck = null;

    private EndPoint epLocal;

    private EndPoint epRemote;

    private byte[] buffer;

   
    private void SendOrder(Order order) 
    {
        Client client = new Client();
        client.sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        client.sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

        client.epLocal = new IPEndPoint(IPAddress.Parse(GetLocalIP()), 11001);
        client.sck.Bind(client.epLocal);
        client.buffer = new byte[4096];

        client.epRemote = new IPEndPoint(IPAddress.Parse(GetLocalIP()), 11000);
        client.sck.Connect(client.epRemote);
        client.sck.BeginReceiveFrom(client.buffer, 0, client.buffer.Length, SocketFlags.None, ref client.epRemote, new AsyncCallback(client.MessageCallBack), client.buffer);

        byte[] mybuffer = new byte[8192];

        try 
        {
            byte[] msg = JsonSerializer.SerializeToUtf8Bytes<object>(order);
            int bytesSent = sck.Send(msg);
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp.Message);
                
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