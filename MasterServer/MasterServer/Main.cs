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
			Console.WriteLine(" >> " + "Welcome to the Pong2D server!");
			Console.WriteLine(" >> " + "Wainting for clients.....");

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
		Thread listenThread1;
		Thread listenThread2;
		string clientNo;

		NetworkStream stream1;
		NetworkStream stream2;
        int mes1, mes2;
        bool start1, start2;
        byte[] send;
		
		public MainServer()
		{
			connectedPlayers = 0;
            start1 = false;
            start2 = false;
			clientList = new TcpClient[maxPlayers];
			listener = new TcpListener(IPAddress.Any, portNumber);
			listenThread1 = new Thread (new ThreadStart (ListendForTCPClients));
			listenThread1.Start ();
			listenThread2 = new Thread (new ThreadStart (ListendForTCPClients));
			listenThread2.Start ();
			
		}
		
		public void ListendForTCPClients()
		{
			this.listener.Start();

			try
			{
				Console.WriteLine(connectedPlayers);
				while(connectedPlayers < maxPlayers)
				{
					TcpClient tcpClient = this.listener.AcceptTcpClient();
					Console.WriteLine(" >> " + "Client No: " + Convert.ToString(connectedPlayers + 1) + " has connected!"); 
					clientList[connectedPlayers] = tcpClient;
					if(connectedPlayers == 0)
					{
						stream1 = clientList[connectedPlayers].GetStream();
						stream1.WriteByte(0);
					}
					else if(connectedPlayers == 1)
					{
						stream2 = clientList[connectedPlayers].GetStream();
						stream2.WriteByte(1);
					}
				


					//handleClient client = new handleClient(); 
					//client.startClient(clientList[connectedPlayers], Convert.ToString(connectedPlayers));

					connectedPlayers++;

				}
				Console.WriteLine("<< 2 clinets have connected to the the Pong2D server");
				Console.WriteLine("<< Waiting for clients to send the start command....");

				while(true)
				{
                    process();
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

        public void process()
        {
            mes1 = 0;
            mes2 = 0;

            mes1 = stream1.ReadByte();
            mes2 = stream2.ReadByte();
            if (mes1 == 255 && !start1)
            {
                start1 = true;
            }
            if (mes2 == 255 && !start2)
            {
                start2 = true;
            }
            if (start1 && start2)
            {
                stream1.WriteByte(0);
                stream2.WriteByte(0);
            }

            Console.WriteLine(" >> Client 1: " + mes1 + System.Environment.NewLine);
            Console.WriteLine(" >> Client 2: " + mes2 + System.Environment.NewLine);

            Console.WriteLine(System.Environment.NewLine);
        }

	}


//	public class handleClient
//	{
//		TcpClient clientSocket;
//		string clientNo;
//		public void startClient(TcpClient inClientSocket, string clientNo)
//		{
//			this.clientSocket = inClientSocket;
//			this.clientNo = clientNo;
//
//		}
//
//		private void ChattingTime()
//		{
//
//			int requestCount = 0;
//			byte[] bytesFrom = new byte[1024];
//			string dataFromClient = null;
//			Byte[] sendBytes = null;
//			string serverResponse = null;
//			string rCount = null;
//			requestCount = 0;
//			
//			String mes1, mes2;
//			while(true)
//			{
//				try
//				{
//					requestCount = requestCount + 1;
//					NetworkStream nws = clientSocket.GetStream();
//					nws.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
//					dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
//					dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
//					Console.WriteLine(" >> " + "From client-" + clientNo + dataFromClient);
//					
//					rCount = Convert.ToString(requestCount);
//					serverResponse = "Server to clinet(" + clientNo + ") " + rCount;
//					sendBytes = Encoding.ASCII.GetBytes(serverResponse);
//					nws.Write(sendBytes, 0, sendBytes.Length);
//					nws.Flush();
//					Console.WriteLine(" >> " + serverResponse);
//				}
//				catch(Exception ex)
//				{
//					Console.WriteLine(" >> " + ex.ToString());
//				}
//
//			}
//		}
//	}
}
