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
			int connected = 0;
			Socket[] players = new Socket[2];

			IPEndPoint ip = new IPEndPoint (IPAddress.Any, port);
			UdpClient listener = new UdpClient (port);
			Socket sock = listener.Client;

			try
			{
				while(connected < 2)
				{
					Console.WriteLine ("Waiting for connetion... ");
					byte[] data = listener.Receive (ref ip);

					Console.WriteLine("Recieved broadcast from {0} :\n {1}\n", ip.ToString(), Encoding.ASCII.GetString(data,0,data.Length));
					Console.WriteLine("Adding {0} to players", ip.ToString());
					players[connected] = listener.Client;
					connected++;
				}

				while(!done)
				{
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