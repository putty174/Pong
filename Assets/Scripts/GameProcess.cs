﻿using UnityEngine;
using System.Collections;

//An example main function
public class GameProcess : MonoBehaviour {

	private GameObject p1;
	private GameObject p2;
	private GameObject ball;

	//PRIVATE MEMBERS
	private Sockets sockets;
	private Client client;
	private GUIScript gui;

	private byte[] buffer;

	// Use this for initialization
	void Start () {

		sockets = new Sockets();
		client = new Client();

		gui = GameObject.Find("GUI").GetComponent<GUIScript>();

		p1 = GameObject.Find ("Player1");
		p2 = GameObject.Find ("Player2");
	}
	
	// Update is called once per frame
	void Update () {

		//Assuming that the implementation is that Game contains the Client.cs code
		//and a main function to make a client object, like GameProcess.cs

		//to make the client object, and the server contains a Server.cs code
		//and and a main function to make a client object, like Main.cs


		//We could use the strings getting sent as input.  

		//Because TCP guarantees everything sent and received in order, we don't need to worry 
		//about naming each string so we can distinguish them.  
		//This, however, is necessary for UDP

		//If UDP
		//client.Send ("Player 1: " + p1.transform.position);
		//client.Send ("Player 2: " + p2.transform.position);

		//client.Send ("Ball's position: " + BallScript.ballPosition.x + ", " 
		//             + BallScript.ballPosition.y + ", " 
		 //            + BallScript.ballPosition.z);

		//OK for TCP
		client.Send (""+p1.transform.position);
		client.Send (""+p2.transform.position);
		
		client.Send (BallScript.ballPosition.x + " " 
		             + BallScript.ballPosition.y + " " 
		             + BallScript.ballPosition.z);



		if(client.receiverBuffer.Count > 0)
		{
			lock(client.receiverBuffer)
			{
				while(client.receiverBuffer.Count > 0)
				{
					buffer = (byte[]) client.receiverBuffer.Dequeue();
					Debug.Log(System.Text.Encoding.ASCII.GetString(buffer));
				}
			}
		}




	}

	public Sockets returnSocket ()
	{
		return sockets;
	}

	public Client returnClient()
	{
		return client;
	}
}
