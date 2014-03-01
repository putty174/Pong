using UnityEngine;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System;
using System.Diagnostics;
using System.Threading;

public class Sockets : MonoBehaviour {

	const string ipAddress = "128.195.11.124";
	const int portNumber = 4000;

	//public UdpClient client;
	public TcpClient client;

	public NetworkStream networkStream;
	public int clientNumber;
	public bool isConnected;

	public Thread thread = null;

	protected static bool ThreadState = false;

	public Queue receiverBuffer;



	IPEndPoint serverRemote;

	public Sockets()
	{

		isConnected = false;

	}

	// Use this for initialization
	void Start () {
		//client = new UdpClient();
		client = new TcpClient();


		//serverRemote = new IPEndPoint(IPAddress.Parse(ipAddress), portNumber);
		

		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public bool Connect()
	{
		try
		{
			//client = new UdpClient(ipAddress, portNumber);
			client = new TcpClient(ipAddress, portNumber);

			serverRemote = new IPEndPoint(IPAddress.Parse(ipAddress), portNumber);
			isConnected = true;

			//sendUDPPacket("ANYONE OUT THERE?");
			SendMessage ("ANYONE OUT THERE?");

			



		}
		catch(Exception e)
		{
		}
		isConnected = true;
		return isConnected;
	}

	public void sendUDPPacket(string toSend)
	{
		try
		{
			byte[] data = Encoding.UTF8.GetBytes(toSend);

			//client.Send(data, data.Length, serverRemote);
			//client.SendTimeout(serverRemote, data);



		}
		catch(Exception e)
		{
		}


	}




}
