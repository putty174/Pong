using UnityEngine;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System;
using System.Diagnostics;
using System.Linq;


using System.Threading;

public class Client : MonoBehaviour {



	const string serverLocation = "128.195.11.124";
	//const int maxLimit = 7;
	const int portNumber = 4000;
	public TcpClient client;
	public NetworkStream nws;
	public Thread t = null;
	IPEndPoint serverEndPoint; 
	public bool isConnected = false;
	public Queue receiverBuffer;//Is assigned in TSock
	
	//Positions of assets
	
	//These objects' properties gets sent to the server.  
	//The server in return sends a message to player 1 and player 2 using this
	
	public GameObject player1;//send player1's position in byte msg
	public GameObject player2;//send player2's position in byte msg
	public GameObject ball;//send ball's position in byte msg
	
	public GameObject gameManager;//_gameManager object.  Send score
	
	public GameObject goal1;//uses it's position that occurs from goalscript.  
	public GameObject goal2;//
	
	public GameObject GUI;//The GUIScript's buttons are used.  <---Don't know if need.  
	//Others need for sure as of now.  
	
	
	//1st person to connect is assigned player 1
	//2nd person to connect is assigned player 2
	
	public static int playerThatClientControls;//if msg is 1, then assigned to player 1.  
	//if msg is 2, then assigned to player 2.  Used in Player1Script.cs and Player2Script.cs
	
	
	public Client()
	{
		serverEndPoint = new IPEndPoint(IPAddress.Parse(serverLocation), portNumber); 

		receiverBuffer = new Queue ();


		uniClock = new Stopwatch();
		dTime = getNTPTime(ref uniClock);
		//dTime = ServerPongEmpty.NTPTime.getNTPTime(ref uniClock);








	}
	
	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
		
		//________SEND POSITIONS TO SERVER HERE_________
		

		
		
	}
	
	public bool Connect()
	{
		try
		{

			client = new TcpClient();

			client.Connect(serverEndPoint);

			nws = client.GetStream();
			TSock ts = new TSock(nws, this);
			t = new Thread(new ThreadStart(ts.process));
			t.IsBackground = true;
			t.Start();
			isConnected = true;
		}
		catch ( Exception ex )
		{
			print ( ex.Message + " : OnConnect");
			
		}
		if ( client == null ) return false;
		
		
		//if(server.numOfClientsConnected = 2) playerThatClientControls = 2; 
		//else playerThatClientControls = 1;
		//if(nws == 1)
//		if(receiverBuffer.Dequeue ().ToString() == "1")
//		{
//			playerThatClientControls = 2;
//		}
//		else
//		{
//			playerThatClientControls = 1;
//		}
		





		
		return client.Connected;
	}
	
	public bool Disconnect()
	{
		try
		{
			nws.Close();
			client.Close();
			if(!client.Connected)
			{
				t.Abort();
				
			}
		}
		catch(Exception ex)
		{
			print ( ex.Message + " : OnDisconnect");
		}
		return true;
	}
	
	public bool StartGame()
	{
		try
		{
			nws.WriteByte(255);
			UnityEngine.Debug.Log("start game");
		}
		catch(Exception ex)
		{
			print ( ex.Message + " : OnStartGame");
		}
		
		
		return true;
	}
	
	public void Send(byte message)
	{
		try
		{
			nws.WriteByte(message);
			Console.WriteLine ("Sent: " + message);
		}
		catch(ArgumentException e)
		{
			Console.WriteLine(e.ToString());
		}
	}

	public void recieve()
	{
		try
		{
			nws.ReadByte();
		}
		catch(Exception e)
		{
			Console.WriteLine(e.ToString());
		}
	}

<<<<<<< HEAD
=======
//<<<<<<< HEAD
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
		while (sock.Receive(ntpData) < 44) { Console.WriteLine("getting NTP"); }
		DateTime T4 = DateTime.UtcNow;
		Console.WriteLine("T4 : = " + T4 + " " + T4.Millisecond);
		//UInt32 destTime = (UInt32)(ntpData[16] << 24) | (UInt32)(ntpData[17] << 16) | (UInt32)(ntpData[18] << 8) | (UInt32)(ntpData[19]);
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
>>>>>>> FETCH_HEAD

//=======

//>>>>>>> FETCH_HEAD

}
