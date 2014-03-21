using UnityEngine;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System;
using System.Diagnostics;
using System.Linq;

public class BallScript : MonoBehaviour {


	public static Vector3 ballPosition;



	// Start speed of the ball
	public float ballSpeed = 10;

	// max speed of the ball
	public float maxSpeed = 20f;

	// increase of speed after each bounce
	public float speedIncrease = .25f;

	// current speed of the ball
	private float currentSpeed;

	// the direction of the ball
	private Vector2 currentBallDirection;

	// A ball reset trigger 
	private bool ballReset = false;


	private int ballIsHit = 1;

	// velocity
	//private int velocityY = rigidbody2D.velocity.y;

	private GameProcess gp;

	// velocity reset
	private int velocityReset;


	private int counter = 0;


	// Use this for initialization
	void Start () { 
		gp = GameObject.Find("_GameManager").GetComponent<GameProcess>();
	}

	void Update()
	{
		//changeBallColor ();

		float x = (GameProcess.ballPosX / GameProcess.paddleRatio) + gp.lPaddle.transform.position.x;
		float y = (GameProcess.ballPosY / GameProcess.wallRatio) + gp.bWall.transform.position.y;

		//calculate new position;
		transform.position = new Vector3 (x,y,0);
	

		//int currentTime = Client.dTime.Millisecond;
//		int currentTime = (Client.dTime.Minute * 60000)+(Client.dTime.Second*1000)+(Client.dTime.Millisecond);
//
//		/*
//		if(currentTime > 999)
//		{
//
//		}
//		else if (currentTime > 99999)
//		{
//		}
//		else if(currentTime > 9999999)
//		{
//		}
//		*/
//
//		//int timeOfCollision = GameProcess.milli;
//		int timeOfCollision = (GameProcess.min * 60000)+(GameProcess.sec*1000)+(GameProcess.milli);
//		int timeStamp = Math.Abs (timeOfCollision - currentTime);
//
//
//
//		int speed = GameProcess.ballVel;
//
//
//		if(counter < timeStamp) counter++;
//
//		transform.Translate (transform.position.x + (speed * counter), transform.position.y + (speed * counter), 0);


		//Not for how this game is structured...
		//calculate dead reckoning position	
		//Vector3 deadReckoningPosition = new Vector3(transform.position.x + speed * (timeStamp), 
		//                                            transform.position.y + speed * (timeStamp),
		//                                            0);




	}

	public void BallStart()
	{
//		var randomNumber = Random.Range(0,2);
//
//		//if collides with player 1, go positive constant direction
//		if(randomNumber <= 0.5)
//		{
//			rigidbody2D.AddForce(new Vector2 (ballSpeed, 10));
//		}
//		else
//		{
//			//if collides with player 2, go negative constant directioin
//			rigidbody2D.AddForce(new Vector2 (-ballSpeed, -10));
//		}
	}

	public void BallReset()
	{
		rigidbody2D.velocity = new Vector2(0,0);
		transform.position = Vector3.zero;
	}

	void OnCollisionEnter2D(Collision2D colInfo)
	{
		GameObject pad = colInfo.gameObject;
		ContactPoint2D[] points = colInfo.contacts;
		float y = points [0].point.y - pad.transform.position.y;
        //gp.sendPositions(); //send positions immediately upon collision

		if (y > 0.5)
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Mathf.Abs(rigidbody2D.velocity.y));
			transform.audio.Play();
		}
		else if (y < -0.5)
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -Mathf.Abs(rigidbody2D.velocity.y));
			transform.audio.Play ();
		}
//
//		var randomUpAngle1 = Random.Range (0,80);//Random angle away from player 1 if ball hits top of paddle
//		var randomDownAngle1 = Random.Range (-80, 0);//Random angle away from player 1 if ball hits bottom of paddle
//		
//		var randomUpAngle2 = Random.Range (100, 180);
//		var randomDownAngle2 = Random.Range (-180, -100);
//
//
//
//
//
//		if(colInfo.collider.tag == "Player1")
//		{
//			//if(true)
//			//{
//			//}
//
//			//else{
//			var velocityY = rigidbody2D.velocity.y;
//			velocityY = velocityY/2 + colInfo.collider.rigidbody2D.velocity.y/3;
//			//}
//
//			ballIsHit = ballIsHit * -1;
//
//
//		}
//		if(colInfo.collider.tag == "Player1Top")
//		{
//			transform.Rotate (0,0,randomUpAngle1);
//		}
//		if(colInfo.collider.tag == "Player1Bottom")
//		{
//			transform.Rotate (0,0,randomDownAngle1);
//		}
//
//
//
//
//		if(colInfo.collider.tag == "Player2")
//		{
//			//if(true)
//			//{
//			//}
//			//else{
//			var velocityY = rigidbody2D.velocity.y * -1.0f;
//			velocityY = velocityY/2 + colInfo.collider.rigidbody2D.velocity.y/3;
//
//			ballIsHit = ballIsHit * -1;
//
//			//}
//		}
//		if(colInfo.collider.tag == "Player2Top")
//		{
//			transform.Rotate (0,0,randomUpAngle2);
//		}
//		if(colInfo.collider.tag == "Player2Bottom")
//		{
//			transform.Rotate (0,0,randomDownAngle2);
//		}
//
//	
	}

	public void position(int x, int y)
	{
		transform.position = new Vector3 (x, y, 0);
	}

	void changeBallColor()
	{
		if(ballIsHit == -1)
		{
			transform.GetComponent<SpriteRenderer>().color = Color.green;
		}

		if(ballIsHit == 1)
		{
			transform.GetComponent<SpriteRenderer>().color = Color.white;
		}
	}



//	void OnCollisionEnter2D(Collision2D colInfo)
//	{
//		if(colInfo.collider.tag == "Player")
//		{
//			var velocityY = rigidbody2D.velocity.y;
//			velocityY = velocityY/2 + colInfo.collider.rigidbody2D.velocity.y/3;
//		}
//
//	}
	


//	void OnTriggerEnter2D(Collider2D otherGameObject)
//	{
//		
//		
//		if(otherGameObject.tag == "Goal" )
//		{
//			Debug.Log ("Goal");
//			
//			StartCoroutine(BallReset());
//			
//			otherGameObject.BroadcastMessage("Goal", SendMessageOptions.DontRequireReceiver);
//			
//		}
//	}








}




