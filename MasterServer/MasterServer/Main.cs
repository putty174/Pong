using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Threading;
using System.Text;
using System.Net.Sockets;
using System.Linq;


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
		
		public void ListendForTCPClients()
		{
			this.listener.Start();

			Console.WriteLine(" >> " + "Welcome to the Pong2D server!");
			Console.WriteLine(" >> " + "Wainting for clients.....");

			try
			{
				while(connectedPlayers <= maxPlayers)
				{
					TcpClient client = this.listener.AcceptTcpClient();
					++connectedPlayers;
					Console.WriteLine(" >> " + "Client No: " + Convert.ToString(connectedPlayers) + " has connected!"); 
					clientList[connectedPlayers] = client;
					
				}
				Console.WriteLine("<< 2 clinets have connected to the the Pong2D server");
				Console.WriteLine("<< Waiting for clients to send the start command....");
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			finally
			{
				this.listener.Stop();
			}




			
			
		}
	}
}
