using UnityEngine;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System;

using System.Threading;

public class Client : MonoBehaviour {
	
	const string serverLocation = "128.195.11.124";
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
		if(receiverBuffer.Dequeue ().ToString() == "1")
		{
			playerThatClientControls = 2;
		}
		else
		{
			playerThatClientControls = 1;
		}
		
		
		
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
			Debug.Log("start game");
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
}
