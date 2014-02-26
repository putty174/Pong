using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


using System.Collections.Generic;
using System.Linq;
using System.IO;




namespace PongServer
{
	/*
	class PongServer
	{
		public static void Main()
		{
			bool done = false;
			int port = 4000;
			byte[] data;

			IPEndPoint ip = new IPEndPoint (IPAddress.Any, port);
			UdpClient listener = new UdpClient (ip);
			IPEndPoint send = new IPEndPoint (IPAddress.Any, 0);

			try
			{
				while(!done)
				{
					data = listener.Receive(ref send);

					Console.WriteLine("Message recieved from {0}: ", send.ToString());
					Console.WriteLine(Encoding.ASCII.GetString(data, 0, data.Length));
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
			finally
			{
				listener.Close();
			}
		}
	}
	*/


	class PongServer
	{
		static void Main(string[] args)
		{
			int recv;
			byte[] data = new byte[1024];//Creates a byte array of data.  Total of 1024 bytes


			//Using IPAddress.Any instead of 128.195.11.124
			IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 4000);//Listens from any connection on
			//port 4000.  Port 4000 is what we'll use for the client

			//Store the connection we got from the client
			Socket newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			//Bind any incoming connection to that socket
			newSocket.Bind (endpoint);




			Console.WriteLine ("Waiting for a client........");//Print to console.  Debug

			//Create a sender to send to the client that we just referenced (named 'endpoint');
			IPEndPoint sender = new IPEndPoint(IPAddress.Any, 4000);//At this point, program will STOP and wait 
			//for an incoming connection.  

			//As soon as any client connects to the tmp remote of  this Port #,
			EndPoint tmpRemote = (EndPoint)sender;//Stores client into this 'tmpRemote' variable


			//Receive the original data from the tmp remote (hence the pass by reference)
			recv = newSocket.ReceiveFrom(data, ref tmpRemote);

			//Debug
			Console.Write ("Msg received from (0)", tmpRemote.ToString ());//Where we received the data from
			Console.WriteLine (Encoding.ASCII.GetString(data, 0, recv));//Convert the received binary into a string and
			//display it into a console


			//______Sending data_______

			string welcome = "Welcome to my server";
			data = Encoding.ASCII.GetBytes (welcome);//Example trying it out. Convert data into a byte array (array of bits)  






			if(newSocket.Connected)//If this client object is connected,
			{
				newSocket.Send (data);//Send the data object from this server to that client object
			}







		}


	}






}