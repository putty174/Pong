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
		
		public MainServer()
		{
			connectedPlayers = 0;
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
						stream1 = clientList[connectedPlayers].GetStream;
						stream1.WriteByte(0);
					}
					else if(connectedPlayers == 1)
					{
						stream2 = clientList[connectedPlayers].GetStream;
						stream2.WriteByte(1);
					}
				


					//handleClient client = new handleClient(); 
					//client.startClient(clientList[connectedPlayers], Convert.ToString(connectedPlayers));

					connectedPlayers++;

				}
				Console.WriteLine("<< 2 clinets have connected to the the Pong2D server");
				Console.WriteLine("<< Waiting for clients to send the start command....");

				byte[] data1 = new byte[1024];
				byte[] data2 = new byte[1024];
				byte[] send;

				String mes1, mes2;
				while(true)
				{
					mes1 = "";
					mes2 = "";
					NetworkStream stream1 = clientList[0].GetStream();
					NetworkStream stream2 = clientList[1].GetStream();

					stream1.Read(data1,0,data1.Length);
					mes1 = System.Text.Encoding.ASCII.GetString(data1,0,data1.Length);
					send = Encoding.ASCII.GetBytes("Client 1");
					stream1.Write (send,0,send.Length);
					stream2.Write(send,0,send.Length);
					Console.WriteLine(" >> Client 1: " + mes1 + System.Environment.NewLine);

					stream2.Read(data2,0,data2.Length);
					mes2 = System.Text.Encoding.ASCII.GetString(data2,0,data1.Length);
					send = Encoding.ASCII.GetBytes("Client 2");
					stream1.Write (send,0,send.Length);
					stream2.Write(send,0,send.Length);
                    Console.WriteLine(" >> Client 2: " + mes2 + System.Environment.NewLine);

					Console.WriteLine(System.Environment.NewLine);
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
