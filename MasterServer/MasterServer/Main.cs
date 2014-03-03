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
		const int maxPlayers = 2;
		const int portNumber = 4000;
		int connectedPlayers = 0;
		TcpListener listener = new TcpListener(IPAddress.Any, portNumber);
		TcpClient[] clientList = new TcpClient[maxPlayers];
		Thread listenThread1;
		Thread listenThread2;

		public static void Main (string[] args)
		{
			Console.WriteLine(" >> " + "Welcome to the Pong2D server!");
			Console.WriteLine(" >> " + "Wainting for clients.....");

			listenThread1 = new Thread (new ThreadStart (ListendForTCPClients));
			listenThread2 = new Thread (new ThreadStart (ListendForTCPClients));

			listenThread1.Start ();
			listenThread2.Start ();

			Console.WriteLine("<< 2 clinets have connected to the the Pong2D server");
			Console.WriteLine("<< Waiting for clients to send the start command....");
			
			byte[] data1 = new byte[1024];
			byte[] data2 = new byte[1024];
			
			String mes1, mes2;
			while(true)
			{
				NetworkStream stream1 = clientList[0].GetStream();
				NetworkStream stream2 = clientList[1].GetStream();
				
				for(int i = stream1.Read(data1,0,data1.Length);i != 0; i = stream1.Read(data1,0,data1.Length))
				{
					mes1 = System.Text.Encoding.ASCII.GetString(data1,0,i);
					Console.WriteLine("Recieved: {0}", mes1);
				}
				for(int i = stream2.Read(data2,0,data2.Length);i != 0; i = stream2.Read (data2,0,data2.Length))
				{
					mes2 = System.Text.Encoding.ASCII.GetString(data2,0,i);
					Console.WriteLine("Recieved: {0}", mes2);
				}
				Console.WriteLine("");
			}
		}

		public void ListendForTCPClients()
		{
			listener.Start();
			
			try
			{
				while(connectedPlayers <= maxPlayers - 1)
				{
					TcpClient client = this.listener.AcceptTcpClient();
					Console.WriteLine(" >> " + "Client No: " + Convert.ToString(connectedPlayers + 1) + " has connected!"); 
					clientList[connectedPlayers] = client;
					connectedPlayers++;
				}
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
