using UnityEngine;
using System;
using System.Collections;

//An example main function
public class GameProcess : MonoBehaviour {

	public int min;
	public int sec;
	public int milli;
	
	private GameObject ball;
	private Player1 p1;
	private Player2 p2;
	private BallScript bscript;
	public GameObject bWall;
	public GameObject lPaddle;

	//private int player = -1;
	public static int player = -1;

	//PRIVATE MEMBERS
	private Sockets sockets;
	private Client client;
	private GUIScript gui;
    private Vector2 collisionLoc; //collision location

	public bool gameStart;
	private byte[] buffer;
    public static int opPosY;
    public static int opVel;
    public static int ballPosX;
    public static int ballPosY;
	public static int ballVel;
    private int angle;
    private int time;
    private Vector2 hit;
    private Vector2 pointOfCollision;
	public static float wallRatio;
	public static float paddleRatio;

	// Use this for initialization
	void Start () {

		sockets = new Sockets();
		client = new Client();
		opPosY = 128;
		ballPosX = 0;
		ballPosY = 0;
		buffer = new byte[6];
		gameStart = false;

		gui = GameObject.Find("GUI").GetComponent<GUIScript>();

		p1 = (Player1) GameObject.Find ("Player1").GetComponent ("Player1");
		p2 = (Player2) GameObject.Find ("Player2").GetComponent ("Player2");
		bscript = (BallScript) GameObject.Find ("GameBall").GetComponent("BallScript");
		lPaddle = GameObject.Find ("Goal2");
		bWall = GameObject.Find ("BottomWall");
		paddleRatio = (250.0f / (GameObject.Find("Goal1").transform.position.x - GameObject.Find("Goal2").transform.position.x));
		wallRatio = (250.0f / (GameObject.Find ("TopWall").transform.position.y - bWall.transform.position.y));
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


		//Debug.Log (client.receiverBuffer.Count);
		if(client.receiverBuffer.Count > buffer.Length)
		{
			lock(client.receiverBuffer)
			{
				while(client.receiverBuffer.Count > buffer.Length)
				{
					//Debug.Log("Queue count: " + client.receiverBuffer.Count);
					opPosY = (int) client.receiverBuffer.Dequeue();
					ballPosX = (int) client.receiverBuffer.Dequeue();
					ballPosY = (int) client.receiverBuffer.Dequeue();
					min = (int) client.receiverBuffer.Dequeue();
					sec = (int) client.receiverBuffer.Dequeue();
					milli = (int) client.receiverBuffer.Dequeue();

                    if (player == -1)
                    {
						if(opPosY == 0)
						{
							player = 1;
							Debug.Log("Player 1");
						}
						else if(opPosY == 1)
						{
							player = 2;
							Debug.Log("Player 2");
						}
						opPosY = 128;
                    }
                    else
                    {
                        //Stores information on opponent position (Y), 
                        //opponent velocity, ball position (X, Y),
                        //angle of ball, server time.
//						if(client.receiverBuffer.Count > 6)
//						{
//                        	opPosY = (int)client.receiverBuffer.Dequeue();
//	                        opVel = (int)client.receiverBuffer.Dequeue();
//	                        ballPosX = (int)client.receiverBuffer.Dequeue();
//	                        ballPosY = (int)client.receiverBuffer.Dequeue();
//							ballVel = (int) client.receiverBuffer.Dequeue();
//	                        angle = (int)client.receiverBuffer.Dequeue();
//	                        time = (int)client.receiverBuffer.Dequeue();
//
//							Debug.Log(opPosY);
//
//							bscript.position(ballPosX,ballPosY);
//						}
						//                        //Stores information on opponent position (Y), 
						//opponent velocity, ball position (X, Y),
						//angle of ball, server time.
						//opPosY = ((float)(client.receiverBuffer.Dequeue())) / wallRatio;
//						if(player == 1)
//						{
//							p2.position((opPosY / wallRatio) + bWall.transform.position.y);
//						}
//						else if(player == 2)
//						{
//							p1.position((opPosY / wallRatio) + bWall.transform.position.y);
//						}
						//Debug.Log("opponent position: " + opPosY);

						//ballPosX = (int) client.receiverBuffer.Dequeue();
						//ballPosY = (int) client.receiverBuffer.Dequeue();
						//angle = (int) client.receiverBuffer.Dequeue();
						//ballVel = (int) client.receiverBuffer.Dequeue();
                    }
				}
			}
		}
	}
    //return estimated time of collision.
    //Parameters (ball position, ball angle, ball velocity)
//    public int timeOfcollide(Vector2 pos, int angle, int velocity)
//    {
//        float dirX = (float)(velocity * Mathf.Cos(angle));
//        float dirY = (float)(velocity * Mathf.Sin(angle));
//        Vector2 dir = new Vector2(dirX, dirY);
//
//        ball.transform.position = pos;
//        ball.rigidbody2D.AddForce(dir);
//
//				//get paddle position
//				// offeset so positive (add botwall.y)
//				// then use ratio to convert to 0~250
//				// send to server
//        RaycastHit2D raycastHit = Physics2D.Raycast(pos, dir);
//        pointOfCollision = raycastHit.point;
//
//
//				
//
//				float temp1 = Player1.player1PosY - bWall.transform.position.y;
//				int result = Convert.ToInt32(temp1 * wallRatio);
//				client.Send((byte)result);//player position * (manual byte range / boardwidth)
//
//				//Debug.Log ("Paddle 1 y position sent" + (byte)(temp1 * wallRatio));
//
//				//number 0 to 250 is the number that server recognizes as a position.  
//				//number 251 is recognized as pause in the server
//				//number 252 is ..
//				//etc.  
//        float distance = Vector2.Distance(pos, pointOfCollision) - 0.1f;
//        
//        return (int)(distance / velocity);
//    }

    public bool collide()
    {
        return true;
    }

	public void sendPositions ()
	{
		try
		{
			if(player == 1)
			{
				//send Player1.x
				//send Player1.y
				//client.Send ((byte)Player1.player1PosX);
				//Debug.Log ("Paddle 1 x position sent"+(byte)Player1.player1PosX);
				
				//get paddle position
				// offeset so positive (add botwall.y)
				// then use ratio to convert to 0~250
				// send to server
				
				float pos = (Player1.player1PosY - bWall.transform.position.y) * wallRatio;
				int result = Convert.ToInt16(pos);
				//Debug.Log ("Player Position: " + result);
				//Debug.Log("Player 1 Position: " + result);
				client.Send((byte)result);//player position * (manual byte range / boardwidth)



				
				//Debug.Log ("Paddle 1 y position sent" + (byte)(temp1 * wallRatio));
				
				//number 0 to 250 is the number that server recognizes as a position.  
				//number 251 is recognized as pause in the server
				//number 252 is ..
				//etc.  
				
				
			}
			else if (player == 2)
			{
				//send Player2.x
				//send Player2.y
				//client.Send ((byte)Player2.player2PosX);
				//Debug.Log ("Paddle 2 x position sent"+(byte)Player2.player2PosX);

				float temp1 = Player2.player2PosY - bWall.transform.position.y;
				int result = Convert.ToInt32(temp1 * wallRatio);
				//Debug.Log("Player 2 Position: " + result);
				client.Send ((byte)result);//player position * (manual byte range / boardwidth)

				//Debug.Log ("Paddle 2 y position sent"+(byte)Player2.player2PosY);
				//Debug.Log ("Paddle 1 y position sent"+(byte)((int)(Player2.player2PosY * (250/13))));
				
				
			}
			
		}
		catch(Exception ex)
		{
			print ( ex.Message + " : Sending positions" );
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
