using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Threading;
using System.Text;
using System.Net.Sockets;


namespace MasterServer
{
	class MainClass
	{

		public static void Main (string[] args)
		{
			
			MainServer ms = new MainServer();
			
		}





	}

	class MainServer : MainClass
	{
		const int maxPlayers = 2;
		const int portNumber = 4000;
		int connectedPlayers;
		TcpListener listener;
		TcpClient[] clientList;
		Thread listenThread;
		
		
		public MainServer()
		{
			connectedPlayers = 0;
			clientList = new TcpClient[maxPlayers];
			listener = new TcpListener(IPAddress.Any, portNumber);
			listenThread = new Thread(new ThreadStart(ListendForTCPClients));
			listenThread.Start();
			
			
		}
		
		private void ListendForTCPClients()
		{
			this.listener.Start();
			
			while(connectedPlayers <= maxPlayers)
			{
				TcpClient client = this.listener.AcceptTcpClient();
				Console.WriteLine("Connected to the server!");
				++connectedPlayers;
				clientList[connectedPlayers] = client;
			}
			
			
		}
	}
}
