using UnityEngine;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System;

using System.Threading;

public class Client : MonoBehaviour {

	const string serverLocation = "192.168.1.107";
	const int portNumber = 4000;
	public TcpClient client;
	public NetworkStream nws;
	public Thread t = null;
	IPEndPoint serverEndPoint; 

	public Client()
	{
		serverEndPoint = new IPEndPoint(IPAddress.Parse(serverLocation), portNumber); 
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool Connect()
	{
		try
		{
			client = new TcpClient();
			client.Connect(serverEndPoint);
			if(client.Connected)
			{
				Debug.Log ("I have Connected!");
			}

		}
		catch ( Exception ex )
		{
			print ( ex.Message + " : OnConnect");
			
		}
		if ( client == null ) return false;
		return client.Connected;
	}
}
