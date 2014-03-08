using UnityEngine;
using System.Collections;

//An example main function
public class GameProcess : MonoBehaviour {

	private GameObject p1;
	private GameObject p2;
	private GameObject ball;
	private BallScript bscript;

	private int player = -1;

	//PRIVATE MEMBERS
	private Sockets sockets;
	private Client client;
	private GUIScript gui;

	private int buffer;

	// Use this for initialization
	void Start () {

		sockets = new Sockets();
		client = new Client();

		gui = GameObject.Find("GUI").GetComponent<GUIScript>();

		p1 = GameObject.Find ("Player1");
		p2 = GameObject.Find ("Player2");
		ball = GameObject.Find ("GameBall");
		bscript = (BallScript) ball.GetComponent("BallScript");
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
//		client.Send (""+p1.transform.position);
//		client.Send (""+p2.transform.position);
//		
//		client.Send (BallScript.ballPosition.x + " " 
//		             + BallScript.ballPosition.y + " " 
//		             + BallScript.ballPosition.z);



		if(client.receiverBuffer.Count > 0)
		{
			lock(client.receiverBuffer)
			{
				while(client.receiverBuffer.Count > 0)
				{
					buffer = (int) client.receiverBuffer.Dequeue();
					switch(buffer)
					{
					case 0:
						if(player == -1)
						{
							player = 1;
							Debug.Log("Player 1");
						}
						else
						{
							bscript.BallStart();
							Debug.Log("Start");
						}
						break;
					case 1:
						if(player == -1)
						{
							player = 2;
							Debug.Log("Player 2");
						}
						break;
					}
				}
			}
		}
	}

	public void win(int player)
	{
		if(player == 1)
		{
			client.Send (0);
		}
		else
		{
			client.Send (1);
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
