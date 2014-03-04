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

				nws = client.GetStream();

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

	public void Send(String message)
	{
		try
		{
			Byte[] data = System.Text.Encoding.ASCII.GetBytes (message);
			nws.Write (data, 0, data.Length);
			Console.WriteLine ("Sent: " + message);
		}
		catch(ArgumentException e)
		{
			Console.WriteLine(e.ToString());
		}
	}

	public String Recieve()
	{
		String message = null;
		try
		{
			Byte[] data = new byte[256];
			Int32 bytes = nws.Read(data,0,data.Length);
			message = System.Text.Encoding.ASCII.GetString(data,0,bytes);
			Console.WriteLine("Recieved: " + message);
			return message;
		}
		catch (ArgumentException e)
		{
			Console.WriteLine ("Exception: " + e.ToString());
		}
		return message;
	}
}
