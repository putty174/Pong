using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PongServer
{
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
}