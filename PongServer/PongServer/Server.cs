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
			int attempt = 0;

			IPEndPoint ip = new IPEndPoint (IPAddress.Any, port);
			UdpClient listener = new UdpClient (port);

			try
			{
				while(!done)
				{
					attempt++;
					Console.WriteLine ("Waiting for connetion... ");
					byte[] data = listener.Receive (ref ip);
					Console.WriteLine("Attempt: " + attempt);

					Console.WriteLine("Recieved broadcast from {0} :\n {1}\n", ip.ToString(), Encoding.ASCII.GetString(data,0,data.Length));
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