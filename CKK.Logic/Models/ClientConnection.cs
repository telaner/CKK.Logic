using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class ClientConnection
    {
        private Socket sck = null;

        private EndPoint epLocal;

        private EndPoint epRemote;

        private byte[] buffer;

        private string receivedMessage;

        private Order order1;

        public ClientConnection(Order order) 
        {
            order1 = order;
        }

        public string OrderProcess() 
        {
            Startup();
            SendOrder(order1);
            Receive();
            return receivedMessage;
        }


        private void Startup()
        {
            try
            {
                sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                epLocal = new IPEndPoint(IPAddress.Parse(GetLocalIP()), 11001);
                sck.Bind(epLocal);
                buffer = new byte[4096];

                epRemote = new IPEndPoint(IPAddress.Parse(GetLocalIP()), 11000);
                sck.Connect(epRemote);
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
                    receivedMessage = eEncoding.GetString(receivedData);

                }

                sck.Shutdown(SocketShutdown.Both);
                sck.Close();

            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
            }
        }

        private void Receive() 
        {
            buffer = new byte[1500];
            var done = false;
            var message = sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
            
            while(done == false) 
            {
                if (receivedMessage != null && message.IsCompleted == true) 
                {
                    done = true;
                }

                else if (receivedMessage == "Error, Could not process order") 
                {
                    done = true;
                    sck.Close();
                }
            }
        }
        private void SendOrder(Order order) 
        {
            

            try
            {
                byte[] msg = JsonSerializer.SerializeToUtf8Bytes<object>(order);
                sck.Send(msg);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);

            }
        }
    }
}
