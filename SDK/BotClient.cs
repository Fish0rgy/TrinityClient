
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Area51.SDK
{
    public class BotClient
    {
        public BotClient()
        {
        }
        private UdpClient client = new UdpClient();
        public string data = "";
        public byte[] sendBytes, rcvPacket = new Byte[1024];
        public IPAddress localhost = IPAddress.Parse("127.0.0.1");
        public IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);

        public void Connect(int port)
        {
            client.Connect(localhost, port);
 
        }
        public void Close()
        {
            client.Close();
        }
        public void Send(string input)
        {
            sendBytes = Encoding.ASCII.GetBytes(input);
            client.Send(sendBytes, sendBytes.GetLength(0));   
        }

        public string OnRecv()
        {
            rcvPacket = client.Receive(ref remoteIPEndPoint);
            string rcvData = Encoding.ASCII.GetString(rcvPacket);
            client.Client.Blocking = false;
            return rcvData;
        }
    }
}

