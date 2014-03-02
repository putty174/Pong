using UnityEngine;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System;
//using System.Diagnostics;
using System.Threading;

public class Sockets : MonoBehaviour {

	// Port and IP data for socket client

	const string ipAddress = "128.195.11.124";
	const int portNumber = 4000;

<<<<<<< HEAD
	public UdpClient udpClient;
=======
	//public UdpClient client;
	public TcpClient client;
>>>>>>> FETCH_HEAD

	public NetworkStream networkStream;
	public int clientNumber;
	public bool isConnected;

	public Thread thread = null;

	protected static bool ThreadState = false;

	public Queue receiverBuffer;


	IPEndPoint serverRemote; 
	//Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
	//IPEndPoint sender = new IPEndPoint(IPAddress.Parse(ipAddress), portNumber);


	EndPoint tmpRemote;

	public Sockets()
	{


		isConnected = false;


<<<<<<< HEAD
=======
	// Use this for initialization
	void Start () {
		//client = new UdpClient();
		client = new TcpClient();
>>>>>>> FETCH_HEAD



	}

	public void sendUDPPacket(string toSend)
	{
		
		try
		{
<<<<<<< HEAD
			byte[] packetData = Encoding.ASCII.GetBytes(toSend);
=======
			//client = new UdpClient(ipAddress, portNumber);
			client = new TcpClient(ipAddress, portNumber);

			serverRemote = new IPEndPoint(IPAddress.Parse(ipAddress), portNumber);
			isConnected = true;

			//sendUDPPacket("ANYONE OUT THERE?");
			SendMessage ("ANYONE OUT THERE?");

>>>>>>> FETCH_HEAD
			
			udpClient.Send(packetData, packetData.Length);
		}
		catch(Exception ex)
		{
			print (ex.Message + " : OnPacket");
		}
		
		
	}


	public bool Connect()
	{
		try
		{
<<<<<<< HEAD
			serverRemote = new IPEndPoint(IPAddress.Parse(ipAddress), portNumber);
			udpClient = new UdpClient();
			udpClient.Connect(serverRemote);
			if(udpClient.Client.Connected)
			{
				Debug.Log("connected!");
				sendUDPPacket("ANYONE OUT THERE?");
=======
			byte[] data = Encoding.UTF8.GetBytes(toSend);

			//client.Send(data, data.Length, serverRemote);
			//client.SendTimeout(serverRemote, data);

>>>>>>> FETCH_HEAD

			}

		}
		catch(Exception ex)
		{
			print ( ex.Message + " : OnConnect");
		}
		isConnected = true;
		if ( udpClient == null ) return false;
		return udpClient.Client.Connected;
	}






}
