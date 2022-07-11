using System;
using System.Net;
using WebSocketSharp;
using WebSocketSharp.Server;
using Trinity.SDK;

namespace Trinity.Sockets
{
	public class WebSocketServ
	{
		public static void StartServer()
		{
			WebSocketServ.websocketServer = new WebSocketServer(IPAddress.Loopback, 9556);
			WebSocketServ.websocketServer.AddWebSocketService<WebSocketServ.BasicWebsocketBehaviour>("/BasicWebsocketBehaviour");
			WebSocketServ.websocketServer.Start();
			LogHandler.Log(LogHandler.Colors.Green, "[Bot Server]: Server Started on ws://127.0.0.1:9556/BasicWebsocketBehaviour.");
			LogHandler.Log(LogHandler.Colors.Yellow, "[Bot Server]: Wating for connection...");
		}

		public static WebSocketServer websocketServer;

		public class BasicWebsocketBehaviour : WebSocketBehavior
		{
			protected override void OnMessage(MessageEventArgs e)
			{
				LogHandler.Log(LogHandler.Colors.DarkBlue, "[Bot Server] [Message]: " + e.Data.ToString());
				WebSocketServ.websocketServer.WebSocketServices["/BasicWebsocketBehaviour"].Sessions.Broadcast(e.Data.ToString());
			}
		}
	}
}