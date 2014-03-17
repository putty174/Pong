
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


using System.Collections.Generic;
using System.Linq;
using System.IO;




namespace Server
{

	//Using TCP Server now

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
	
	
	class Server
	{
		

		static void Main(string[] args)
		{

			
			bool done = false;
			int port = 4000;
			byte[] data;
			
			IPEndPoint ip = new IPEndPoint (IPAddress.Any, port);
			TcpListener listener = new TcpListener(port);
			IPEndPoint send = new IPEndPoint (IPAddress.Any, 0);




			Socket soc = listener.AcceptSocket (); 


			Console.WriteLine ("Connected: {0}", soc.RemoteEndPoint);


			try
			{
				while(!done)
				{

					NetworkStream nws = new NetworkStream(soc);

					StreamReader sr = new StreamReader(nws);
					StreamWriter sw = new StreamWriter(nws);

					sw.AutoFlush = true;



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
	
	
	
	
	
	
}
