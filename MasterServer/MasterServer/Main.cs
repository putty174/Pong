using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Threading;
using System.Text;
using System.Net.Sockets;
using System.Linq;
using System.Diagnostics;


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
		public static Stopwatch uniClock;
		public static DateTime dTime;
        public static DateTime dTimeNew;
		const int maxPlayers = 2;
		const int portNumber = 4000;
		int connectedPlayers;
		TcpListener listener;
		TcpClient[] clientList;
		Thread listenThread1;
		Thread listenThread2;

        int dstart1, dstart2;
        bool start1, start2;

		NetworkStream stream1;
		NetworkStream stream2;
        int mes1, mes2;

        int startDelay = 100;
        TimeSpan delay1, delay2;
        DateTime lastTime;
        Random rand = new Random();

        int pos1, pos2;
        double nposx, nposy;
        double collideX, collideY;
        int checkCollide;
        DateTime collideTime;
        double angle;
        double vel;

        int leftPaddlePad, rightPaddlePad, topWallPad, botWallPad;

        byte[] packet1;
        byte[] packet2;
        byte[] milliHold;
		
		public MainServer()
		{
            leftPaddlePad = 30;
            rightPaddlePad = 30;
            topWallPad = 10;
            botWallPad = 8;

			//dTime = getNTPTime(ref uniClock);

            uniClock = new Stopwatch();

            dTime = getNTPTime(ref uniClock);

            
			connectedPlayers = 0;
            start1 = false;
            start2 = false;
			clientList = new TcpClient[maxPlayers];
            packet1 = new byte[8];
            packet2 = new byte[8];
            milliHold = new byte[2];

            //IPHostEntry host;
            //IPAddress thisComputer;
            //thisComputer = Dns.GetHostEntry("127.0.0.1").AddressList[0];
            //listener = new TcpListener(thisComputer, portNumber);

            listener = new TcpListener(IPAddress.Any, portNumber);
			listenThread1 = new Thread (new ThreadStart (ListendForTCPClients));
			listenThread1.Start ();
			listenThread2 = new Thread (new ThreadStart (ListendForTCPClients));
			listenThread2.Start ();
			
            restart();
		}

        public void restart()
        {

            dstart1 = 0;
            dstart2 = 0;
            checkCollide = 0;
            nposx = rand.Next(0, 250);
            nposy = rand.Next(0, 250);
            angle = rand.NextDouble() * 2 * Math.PI;
            vel = .5;
            packet1[0] = (byte) 0;
            packet1[1] = (byte) 0;
            packet1[2] = (byte) 0;
            dTime = getNTPTime(ref uniClock);
            packet1[3] = (byte)dTime.Minute;
            packet1[4] = (byte)dTime.Second;
            milliHold = BitConverter.GetBytes(dTime.Millisecond);
            packet1[5] = milliHold[0];
            packet1[6] = milliHold[1];

            packet2[0] = (byte) 1;
            packet2[1] = (byte) 0;
            packet2[2] = (byte) 0;
            dTime = getNTPTime(ref uniClock);
            packet1[3] = (byte)dTime.Minute;
            packet1[4] = (byte)dTime.Second;
            milliHold = BitConverter.GetBytes(dTime.Millisecond);
            packet1[5] = milliHold[0];
            packet1[6] = milliHold[1];

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
                        packet1[0] = 0;
                        stream1.Write(packet1, 0, packet1.Length);
                        packet1[0] = 128;
                    }
                    else if (connectedPlayers == 1)
                    {
                        stream2 = clientList[connectedPlayers].GetStream();
                        packet2[0] = 1;
                        stream2.Write(packet2, 0, packet2.Length);
                        packet2[0] = 128;
                    }

                    //handleClient client = new handleClient(); 
                    //client.startClient(clientList[connectedPlayers], Convert.ToString(connectedPlayers));

                    connectedPlayers++;
                }

                //Console.WriteLine("Entering: Main Game Loop");
                while (true)
                {
                        Console.WriteLine("Entering: waitReady()");
                        waitReady();
                        Console.WriteLine("Entering: process()");
                        process();
                        Console.WriteLine("Entering: update()");
                        update();
                        Console.WriteLine("Entering: send()");
                        send();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                listenThread1.Abort();
                listenThread2.Abort();
                this.listener.Stop();
            }
		}

        public void waitReady()
        {
            //Console.WriteLine("<< 2 clients have connected to the the Pong2D server");
            //Console.WriteLine("<< Waiting for clients to send the start command....");
            while (!start1 || !start2)
            {
                try
                {
                    mes1 = stream1.ReadByte();
                    if (mes1 == 255)
                    {
                        Console.WriteLine("Client 1 has sent Start");
                        start1 = true;
                    }

                    mes2 = stream2.ReadByte();
                    if (mes2 == 255)
                    {
                        Console.WriteLine("Client 2 has sent Start");
                        start2 = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void process()
        {
            try
            {
                pos1 = stream1.ReadByte();
                Console.WriteLine("Player1 Pos: " + pos1);
                if (dstart1 == 1)
                {
                    dTimeNew = getNTPTime(ref uniClock);
                    delay1 = new TimeSpan(dTimeNew.Subtract(dTime).Ticks / 2);
                    Console.WriteLine("P1 delay: " + (delay1.TotalMilliseconds));
                    dstart1 = 2;
                }

                pos2 = stream2.ReadByte();
                Console.WriteLine("Player2 Pos: " + pos2);
                if (dstart2 == 1)
                {
                    dTimeNew = getNTPTime(ref uniClock);
                    delay2 = new TimeSpan(dTimeNew.Subtract(dTime).Ticks / 2);
                    Console.WriteLine("P2 delay: " + (delay2.TotalMilliseconds));
                    dstart2 = 2;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void update()
        {
            if (angle < 0.0)
                angle += 2 * Math.PI;
            else if (angle > 2 * Math.PI)
                angle -= 2 * Math.PI;
            if (dstart1 == 2 && dstart2 == 2)
            {
                nposx = 128;
                nposy = 128;
                dstart1 = 3;
                dstart2 = 3;
            }
            if (start1 && start2)
            {
                nposx += vel * Math.Cos(angle) * DateTime.Now.Subtract(lastTime).Milliseconds;
                //Console.WriteLine("BallX: " + nposx);
                nposy += vel * Math.Sin(angle) * DateTime.Now.Subtract(lastTime).Milliseconds;
                //Console.WriteLine("BallY: " + nposy);
                lastTime = DateTime.Now;
                //Console.WriteLine("Collision at: " + dTime.Minute + ":" + dTime.Second + " . " + dTime.Millisecond);
            }
            //if(nposx < 30)
            //{
            //    nposx = 128;
            //    nposy = 128;
            //}
            //if(nposx > 250)
            //{
            //    nposx = 128;
            //    nposy = 128;
            //}

            if (nposx < leftPaddlePad && checkCollide == 0)
            {
                //nposx = Math.Abs(nposx);
                dTime = getNTPTime(ref uniClock);
                collideTime = DateTime.Now;
                collideX = nposx;
                collideY = nposy;
                //Console.WriteLine("Collision - Left");
                //angle = bounceLeft(angle);
                //nposx = leftPaddlePad;
                checkCollide = 1;
            }
            else if (nposx > 250 - rightPaddlePad && checkCollide == 0)
            {
                //nposx = 250 - (nposx - 250);
                dTime = getNTPTime(ref uniClock);
                collideTime = DateTime.Now;
                collideX = nposx;
                collideY = nposy;
                //Console.WriteLine("Collision - Left");
                //angle = bounceRight(angle);
                //nposx = 250 - rightPaddlePad;
                checkCollide = 2;
            }
            if (nposy < botWallPad)
            {
                //nposy = Math.Abs(nposy);
                angle = bounceBot(angle);
                nposy = botWallPad;
            }
            else if (nposy > 250 - topWallPad)
            {
                //nposy = 250 - (nposy - 250);
                angle = bounceTop(angle);
                nposy = 250 - topWallPad;
            }
            if (checkCollide == 1 || checkCollide == 2)
            {
                nposx = collideX;
                nposy = collideY;
                confirmCollide();
            }

            if (nposx < (leftPaddlePad / 2.0))
            {
                pos1 = 2;
                pos2 = 2;

                 
            }
            else if (nposx > (250.0 - (rightPaddlePad / 2.0)))
            {
                pos1 = 1;
                pos2 = 1;

            }
            //Console.WriteLine("Angle: " + (angle / Math.PI));
        }

        public void confirmCollide()
        {
            if (checkCollide == 1)
            {
                if (TimeSpan.Compare(DateTime.Now.Subtract(collideTime), delay1) == 1)
                {
                    if (Math.Abs(collideY - pos1) < 15)
                    {
                        angle = bounceLeft(angle);
                        nposx = collideX;
                        nposy = collideY;
                        checkCollide = 0;
                        Console.WriteLine("P1 hit");
                    }
                    else
                    {
                        checkCollide = 3;
                        Console.WriteLine("P1 missed");
                    }
                }
                else
                {
                    Console.WriteLine("P1 - Not Yet");
                }
            }


            if (checkCollide == 2)
            {
                if (TimeSpan.Compare(DateTime.Now.Subtract(collideTime), delay2) == 1)
                {
                    if (Math.Abs(collideY - pos2) < 15)
                    {
                        angle = bounceRight(angle);
                        nposx = collideX;
                        nposy = collideY;
                        checkCollide = 0;
                        Console.WriteLine("P2 hit");
                    }
                    else
                    {
                        checkCollide = 3;
                        Console.WriteLine("P2 missed");
                    }
                }
                else
                {
                    Console.WriteLine("P2 - Not Yet");
                }
            }
        }

        public double bounceLeft(double a)
        {
            if (a > (0.5 * Math.PI))
            {
                Console.WriteLine("BounceLeft() - Up");
                return (Math.PI - a);
            }
            else if (a < (1.5 * Math.PI))
            {
                Console.WriteLine("BounceLeft() - Down");
                return (3 * Math.PI - a);
            }
            else
                return a;
        }

        public double bounceRight(double a)
        {
            if (a < (0.5 * Math.PI))
            {
                Console.WriteLine("BounceRight() - Up");
                return (Math.PI - a);
            }
            else if (a > (1.5 * Math.PI))
            {
                Console.WriteLine("BounceRight() - Down");
                return (3 * Math.PI - a);
            }
            else
                return a;
        }

        public double bounceBot(double a)
        {
            if(a > Math.PI)
                return (2 * Math.PI - a);
            else
                return a;
        }

        public double bounceTop(double a)
        {
            if (a < Math.PI)
                return (2 * Math.PI - a);
            else
                return a;
        }

        public void send()
        {
            try
            {
                if (dstart1 == 3 && dstart2 == 3 && startDelay > 0)
                {
                    nposx = 128;
                    nposy = 128;

                    startDelay--;
                }

                int ballx = Convert.ToInt16(nposx);
                int bally = Convert.ToInt16(nposy);

                Console.WriteLine("Writing P1-1: " + pos2);
                packet1[0] = (byte)pos2;
                Console.WriteLine("Writing P1-2: " + ballx);
                packet1[1] = (byte)ballx;
                Console.WriteLine("Writing P1-3: " + bally);
                packet1[2] = (byte)bally;
                //Console.WriteLine("Checking NTP Time");
                //dTime = getNTPTime(ref uniClock);
                Console.WriteLine("Writing P1-4: " + dTime.Minute);
                packet1[3] = (byte)dTime.Minute;
                Console.WriteLine("Writing P1-5: " + dTime.Second);
                packet1[4] = (byte)dTime.Second;
                milliHold = new byte[2];
                milliHold = BitConverter.GetBytes(dTime.Millisecond);
                Console.WriteLine("Writing P1-67: " + dTime.Millisecond);
                packet1[5] = milliHold[0];
                packet1[6] = milliHold[1];

                int milli = BitConverter.ToInt16(milliHold, 0);
                Console.WriteLine("<< To Client1: " + packet1[0] + ", " + packet1[1] + ", " + packet1[2] + ", " + packet1[3] + ", " + packet1[4] + ", " + milli);

                Console.WriteLine("Writing P2-1 " + pos1);
                packet2[0] = (byte)pos1;
                Console.WriteLine("Writing P2-2: " + ballx);
                packet2[1] = (byte)ballx;
                Console.WriteLine("Writing P2-3: " + bally);
                packet2[2] = (byte)bally;
                //Console.WriteLine("Finished Writing P2-3");
                //dTime = getNTPTime(ref uniClock);
                Console.WriteLine("Writing P2-4: " + dTime.Minute);
                packet2[3] = (byte)dTime.Minute;
                Console.WriteLine("Writing P2-5: " + dTime.Second);
                packet2[4] = (byte)dTime.Second;
                milliHold = new byte[2];
                milliHold = BitConverter.GetBytes(dTime.Millisecond);
                Console.WriteLine("Writing P2-67: " + dTime.Millisecond);
                packet2[5] = milliHold[0];
                packet2[6] = milliHold[1];

                milli = BitConverter.ToInt16(milliHold, 0);
                Console.WriteLine("  << To Client2: " + packet2[0] + ", " + packet2[1] + ", " + packet2[2] + ", " + packet2[3] + ", " + packet2[4] + ", " + milli);

                if (dstart1 == 0 && dstart2 == 0)
                {
                    dTime = getNTPTime(ref uniClock);
                    dstart1 = 1;
                    dstart2 = 1;
                }
                Console.WriteLine("Sending Packet1");
                stream1.Write(packet1, 0, packet1.Length);
                Console.WriteLine("Sending Packet2");
                stream2.Write(packet2, 0, packet2.Length);
                if ((pos1 == 1 && pos2 == 1) || (pos1 == 2 && pos2 == 2))
                {
                    startDelay = 100;
                    nposx = 128;
                    nposy = 128;
                }
                Console.WriteLine(System.Environment.NewLine);
            }
            
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

		public static DateTime getNTPTime( ref Stopwatch uniClock )
		{
			Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			//IPAddress serverAddr = IPAddress.Parse("nist1-la.ustiming.org ");
			
			IPAddress serverAddr = IPAddress.Parse("64.147.116.229");
			
			IPEndPoint endPoint = new IPEndPoint(serverAddr, 123);
			
			byte[] ntpData = new byte[48];
			
			ntpData[0] = 0x1B;
			ulong currSeconds = (ulong)((DateTime.UtcNow - new DateTime(1900, 1, 1)).TotalSeconds);
			ulong currMillisecs = (ulong)((DateTime.UtcNow - new DateTime(1900, 1, 1)).TotalMilliseconds);
			ulong tempSeconds;
			ntpData[40] = (byte)(currSeconds >> 24);
			tempSeconds = currSeconds << 40;
			ntpData[41] = (byte)(tempSeconds >> 56);
			tempSeconds = currSeconds << 48;
			ntpData[42] = (byte)(tempSeconds >> 56);
			tempSeconds = currSeconds << 56;
			ntpData[43] = (byte)(tempSeconds >> 56);
			
			currMillisecs = (ulong)(((UInt32)(currMillisecs / 1000)) * UInt32.MaxValue);
			
			ntpData[44] = (byte)(currMillisecs >> 24);
			tempSeconds = currMillisecs << 40;
			ntpData[45] = (byte)(tempSeconds >> 56);
			tempSeconds = currMillisecs << 48;
			ntpData[46] = (byte)(tempSeconds >> 56);
			tempSeconds = currMillisecs << 56;
			ntpData[47] = (byte)(tempSeconds >> 56);
			
			
			sock.Connect(endPoint);
			DateTime T1 = DateTime.UtcNow;
			Console.WriteLine("T1 : = " + T1 + " " + T1.Millisecond);
			sock.Send(ntpData);
            Console.WriteLine("sock.Send OK!!!");
			while (sock.Receive(ntpData) < 44) { Console.WriteLine("getting NTP"); }
            Console.WriteLine("while loop OK!!!");
			DateTime T4 = DateTime.UtcNow;
			Console.WriteLine("T4 : = " + T4 + " " + T4.Millisecond);
			UInt32 destTime = (UInt32)(ntpData[16] << 24) | (UInt32)(ntpData[17] << 16) | (UInt32)(ntpData[18] << 8) | (UInt32)(ntpData[19]);
			sock.Close();
			
			Console.WriteLine("LI (lead indicator) : " + (ntpData[0] >> 6));
			int temp = ntpData[0] << 2;
			temp = temp >> 5;
			Console.WriteLine("VN (version number) : " + temp);
			temp = (byte)(ntpData[0] << 5);
			temp = temp >> 5;
			Console.WriteLine("Mode : " + temp);
			Console.WriteLine("Stratum Level : " + ntpData[1]);
			Console.WriteLine("Poll Interval :  " + ntpData[2]);
			Console.WriteLine("Precision : " + ntpData[3]);
			
			/*
These are the 4 time stamps that are retrieved from the NTP Packet.
More on the details of these packets can be researched from the NTP documentation.

Reference Timestamp: This is the local time at which the local clock was last set or corrected, in
64-bit timestamp format.

Originate Timestamp: This is the local time at which the request departed the client host for the
service host, in 64-bit timestamp format.

Receive Timestamp: This is the local time at which the request arrived at the service host, in 64-bit
timestamp format.

Transmit Timestamp: This is the local time at which the reply departed the service host for the client
host, in 64-bit timestamp format.

*/
			TimeSpan calculationTime;
			UInt32 refTime = (UInt32)(ntpData[16] << 24) | (UInt32)(ntpData[17] << 16) | (UInt32)(ntpData[18] << 8) | (UInt32)(ntpData[19]);
			UInt32 refTimeMicro = (UInt32)(ntpData[20] << 24) | (UInt32)(ntpData[21] << 16) | (UInt32)(ntpData[22] << 8) | (UInt32)(ntpData[23]);
			UInt32 refTimemilliseconds = (UInt32)(((double)refTimeMicro / UInt32.MaxValue) * 1000);
			
			UInt32 origTime = (UInt32)(ntpData[24] << 24) | (UInt32)(ntpData[25] << 16) | (UInt32)(ntpData[26] << 8) | (UInt32)(ntpData[27]);
			UInt32 origTimeMicro = (UInt32)(ntpData[28] << 24) | (UInt32)(ntpData[29] << 16) | (UInt32)(ntpData[30] << 8) | (UInt32)(ntpData[31]);
			UInt32 origTimemilliseconds = (UInt32)(((double)origTimeMicro / UInt32.MaxValue) * 1000);
			
			UInt32 recTime = (UInt32)(ntpData[32] << 24) | (UInt32)(ntpData[33] << 16) | (UInt32)(ntpData[34] << 8) | (UInt32)(ntpData[35]);
			UInt32 recTimeMicro = (UInt32)(ntpData[36] << 24) | (UInt32)(ntpData[37] << 16) | (UInt32)(ntpData[38] << 8) | (UInt32)(ntpData[39]);
			UInt32 recTimemilliseconds = (UInt32)(((double)recTimeMicro / UInt32.MaxValue) * 1000);
			
			UInt32 transTime = (UInt32)(ntpData[40] << 24) | (UInt32)(ntpData[41] << 16) | (UInt32)(ntpData[42] << 8) | (UInt32)(ntpData[43]);
			UInt32 transTimeMicro = (UInt32)(ntpData[44] << 24) | (UInt32)(ntpData[45] << 16) | (UInt32)(ntpData[46] << 8) | (UInt32)(ntpData[47]);
			UInt32 milliseconds = (UInt32)(((double)transTimeMicro / UInt32.MaxValue) * 1000);
			
			
			DateTime BaseDateExample = new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			Console.WriteLine("Reference time stamp : " + BaseDateExample.AddSeconds(refTime).AddMilliseconds(refTimemilliseconds));
			Console.WriteLine("Originate time stamp : " + BaseDateExample.AddSeconds(origTime).AddMilliseconds(origTimemilliseconds));
			
			
			Console.WriteLine("Receive time stamp   : " + BaseDateExample.AddSeconds(recTime).AddMilliseconds(recTimemilliseconds) + " " + (BaseDateExample.AddSeconds(recTime).AddMilliseconds(recTimemilliseconds)).Millisecond);
			Console.WriteLine("Transmit time stamp  : " + BaseDateExample.AddSeconds(transTime).AddMilliseconds(milliseconds) + " " + (BaseDateExample.AddSeconds(transTime).AddMilliseconds(milliseconds)).Millisecond);
			
			DateTime BaseDate = new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			DateTime dt = BaseDate.AddSeconds(transTime).AddMilliseconds(milliseconds);
			
			
			DateTime BaseDate1 = new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			DateTime T2 = BaseDate1.AddSeconds(recTime).AddMilliseconds(recTimemilliseconds);
			
			calculationTime = dt.Subtract(T2);
			
			
			TimeSpan delay = T4.Subtract(T1);
			double networkDelay = delay.Milliseconds / 2.0;

            Console.WriteLine("Network Latency to NTP Server : " + (networkDelay));
			
			//tsOffset = tsOffset.Add(dt.Subtract(T4));
			
			dt = dt.AddMilliseconds(networkDelay);
			
			uniClock.Start();
			
			return dt;
		}

	}
}
