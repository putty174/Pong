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

        bool startGame;
        bool client1Start = false;
        bool client2Start = false;
        bool start1, start2;

		NetworkStream stream1;
		NetworkStream stream2;
        int mes;

        DateTime startTime;
        DateTime lastTime;
        Random rand = new Random();

        int pos1, pos2;
        int vel1, vel2;
        int time1, time2;
        int col1, col2;
        double oposx, oposy;
        double nposx, nposy;
        double angle;
        int vel;
		
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
            restart();
		}

        public void restart()
        {
            startGame = false;
            oposx = 128;
            oposy = 128;
            nposx = rand.Next(0, 250);
            nposy = rand.Next(0, 250);
            angle = rand.NextDouble() * 2 * Math.PI;
            vel = 10;
        }
		
		public void ListendForTCPClients()
		{
			this.listener.Start();

            try
            {
                while (connectedPlayers < maxPlayers)
                {
                    TcpClient tcpClient = this.listener.AcceptTcpClient();
                    Console.WriteLine(" >> " + "Client No: " + Convert.ToString(connectedPlayers + 1) + " has connected!");
                    clientList[connectedPlayers] = tcpClient;
                    if (connectedPlayers == 0)
                    {
                        stream1 = clientList[connectedPlayers].GetStream();
                        stream1.WriteByte(0);
                    }
                    else if (connectedPlayers == 1)
                    {
                        stream2 = clientList[connectedPlayers].GetStream();
                        stream2.WriteByte(1);
                    }
                    //handleClient client = new handleClient(); 
                    //client.startClient(clientList[connectedPlayers], Convert.ToString(connectedPlayers));

                    connectedPlayers++;

                }

                while (true)
                {
                    waitReady();
                    process();
                    update();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                this.listener.Stop();
            }
		}

        public void waitReady()
        {
            while (!start1 && !start2)
            {
                Console.WriteLine("<< 2 clients have connected to the the Pong2D server");
                Console.WriteLine("<< Waiting for clients to send the start command....");

                mes = stream1.ReadByte();
                if (mes == 255)
                    start1 = true;

                mes = stream2.ReadByte();
                if (mes == 255)
                    start2 = true;
            }
            if (start1 && start2 && !startGame)
            {
                //startTime = DateTime.Now;
                //lastTime = startTime;
                //stream1.WriteByte(255);
                //stream2.WriteByte(255);
                //stream1.WriteByte(128);
                //stream1.WriteByte(0);
                //stream2.WriteByte(128);
                //stream2.WriteByte(0);
                stream1.WriteByte(128);
                stream2.WriteByte(128);
                startGame = true;
            }
        }

        public void process()
        {
            

			//stream 1
			pos2 = stream2.ReadByte();
			stream1.WriteByte((byte)pos2);
			//vel2 = stream2.ReadByte();
			//stream1.WriteByte((byte)vel2);
			//col2 = stream2.ReadByte();
			//time2 = stream2.ReadByte();

			//stream 2
			pos1 = stream1.ReadByte();
			stream2.WriteByte((byte)pos1);
			//vel1 = stream1.ReadByte();
			//stream2.WriteByte((byte)vel1);
			//col1 = stream1.ReadByte();
			//time1 = stream1.ReadByte();

			//stream 1
            //stream1.WriteByte((byte)Convert.ToInt32(nposx));
            //stream1.WriteByte((byte)Convert.ToInt32(nposy));
            //stream1.WriteByte((byte)Convert.ToInt32(vel));
            //stream1.WriteByte((byte)Convert.ToInt32((angle*250) / (2*Math.PI)));
            //stream1.WriteByte((byte)lastTime.Second);
            
			//stream 2
			//stream2.WriteByte((byte)Convert.ToInt32(nposx));
            //stream2.WriteByte((byte)Convert.ToInt32(nposy));
            //stream2.WriteByte((byte)Convert.ToInt32(vel));
            //stream2.WriteByte((byte)Convert.ToInt32((angle*250) / (2*Math.PI)));
           //stream2.WriteByte((byte)lastTime.Second);

            Console.WriteLine(" >> Client 1: " + pos1 + " , " + vel1 + " , " + col1 + " , " + time1);
            Console.WriteLine("    >> Client 2: " + pos2 + " , " + vel2 + " , " + col2 + " , " + time2);
            Console.WriteLine(System.Environment.NewLine);
        }

        public void update()
        {
            nposx += vel * Math.Cos(angle) * DateTime.Now.Subtract(lastTime).Seconds;
            nposy += vel * Math.Sin(angle) * DateTime.Now.Subtract(lastTime).Seconds;

            if (nposx < 0)
            {
                nposx = Math.Abs(nposx);
                angle = changeAngle(angle);
            }
            else if (nposx > 250)
            {
                nposx = 250 - (nposx - 250);
                angle = changeAngle(angle);
            }
            if (nposy < 0)
            {
                nposy = Math.Abs(nposy);
                angle = changeAngle(angle);
            }
            else if (nposy > 250)
            {
                nposy = 250 - (nposy - 250);
                angle = changeAngle(angle);
            }


            lastTime = DateTime.Now;
        }

        public double changeAngle(double a)
        {
            if (a == 0)
                return Math.PI;
            else if (a == Math.PI)
                return 0;
            else
                return (2 - a);
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
