using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Trinity.SDK;

namespace Trinity.Sockets.BotServer
{
    internal class BotServer
    {
        private static int botCount = 2;
        private static List<Socket> ServerHandlers = new List<Socket>();
        public static void SendCommandToClients(string Command)
        {
            LogHandler.Log(LogHandler.Colors.Yellow, $"[{DateTime.Now}] Phoning Home: {Command}", false, false);
            (from s in BotServer.ServerHandlers
             where s != null
             select s).ToList<Socket>().ForEach(delegate (Socket s)
             {
                 s.Send(Encoding.ASCII.GetBytes(Command));
             });
        }


        private static void OnClientReceiveCommand(string Command)
        {         
            switch (Command)
            {
                case "Play":
                  // PhotonClient.Debuglog($"[{DateTime.Now}] Received Transmission From Mothership: ({Command})");
                    break;
                case "Test":
                  //  PhotonClient.Debuglog($"[{DateTime.Now}] Received Transmission From Mothership: ({Command})");
                    break;
            }
        }

        public static void StartServer()
        {
            BotServer.ServerHandlers.Clear();
            BotServer.botCount = 0;
            Task.Run(new Action(BotServer.HandleServer));
        }

        private static void HandleServer()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry("localhost");
            IPAddress ipaddress = hostEntry.AddressList[0];
            IPEndPoint localEP = new IPEndPoint(ipaddress, 1337);
            try
            {
                using (Socket socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Bind(localEP);
                    socket.Listen(10);

                    for (int i = 0; i < BotServer.botCount; i++)
                    {
                        LogHandler.Log(LogHandler.Colors.Yellow, $"Awaiting response from bots -> {BotServer.botCount}!", false, false);
                        BotServer.ServerHandlers.Add(socket.Accept());
                        
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.Log(LogHandler.Colors.Yellow, $"{ex.ToString()}", false, false);                
            }
        }

        public static void Client()
        {
            Task.Run(new Action(BotServer.HandleClient));
        }

        private static void HandleClient()
        {
            byte[] array = new byte[1024];
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry("localhost");
                IPAddress ipaddress = hostEntry.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipaddress, 1337);
                using (Socket socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                {
                    try
                    {
                        socket.Connect(remoteEP);
                        LogHandler.Log(LogHandler.Colors.Yellow, $"Socket connected to {socket.RemoteEndPoint.ToString()}", false, false);                        
                        for (; ; )
                        {
                            int count = socket.Receive(array);
                            BotServer.OnClientReceiveCommand(Encoding.ASCII.GetString(array, 0, count));
                        }
                    }
                    catch (ArgumentNullException ex)
                    {
                        LogHandler.Log(LogHandler.Colors.Yellow, $"Socket Exception : {ex.ToString()}", false, false);
                       
                    }
                    catch (SocketException ex2)
                    {
                        LogHandler.Log(LogHandler.Colors.Yellow, $"Socket Exception : {ex2.ToString()}", false, false);
                       
                    }
                    catch (Exception ex3)
                    {
                        LogHandler.Log(LogHandler.Colors.Yellow, $"Socket Exception : {ex3.ToString()}", false, false);
                       
                    }
                }
            }
            catch (Exception ex4)
            {
                LogHandler.Log(LogHandler.Colors.Yellow, $"Socket Exception : {ex4.ToString()}", false, false);
            }
        }
    }
}
