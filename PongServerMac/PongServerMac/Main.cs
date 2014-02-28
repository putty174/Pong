using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PongServerMac
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			bool done = false;
			int port = 4000;
			int recv;
			byte[] data = new byte[1024];

			// listen for connection
			IPEndPoint endPoint = new IPEndPoint (IPAddress.Any, port);


			// storing the connection we get from the client
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			// bind any connection to the server socket
			serverSocket.Bind(endPoint); 

			Console.WriteLine("Waiting for a client to connect...");

			IPEndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, port);

			EndPoint remote = (EndPoint)senderEndPoint;

			recv = serverSocket.ReceiveFrom(data, ref remote);

			//UdpClient listener = new UdpClient (ip);

			Console.WriteLine("Message recieved from {0}: ", remote.ToString());
			Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

			string fromServer = "Welcome to the Pong server!";
			data = Encoding.ASCII.GetBytes(fromServer);

			if(serverSocket.Connected)
			{
				serverSocket.Send(data);
			}

			while(true)
			{
				if(!serverSocket.Connected)
				{
					Console.WriteLine("Client has been disconnected");
					break;
				}

				data = new byte[1024];
				recv = serverSocket.ReceiveFrom(data, ref remote);

				if(recv == 0)
				{
					break;
				}

				Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
			}

			serverSocket.Close();

//			IPEndPoint send = new IPEndPoint (IPAddress.Any, 0);
//			
//			try
//			{
//				while(!done)
//				{
//					data = listener.Receive(ref send);
//
//					
//					Console.WriteLine("Message recieved from {0}: ", send.ToString());
//					Console.WriteLine(Encoding.ASCII.GetString(data, 0, data.Length));
//				}
//			}
//			catch (Exception e)
//			{
//				Console.WriteLine(e.ToString());
//			}
//			finally
//			{
//				listener.Close();
//			}
		}
	}
}
