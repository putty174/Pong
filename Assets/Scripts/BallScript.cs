using UnityEngine;
using System.Collections;

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



	public GameObject player1Top;
	public GameObject player1Bottom;
	public GameObject player2Top;
	public GameObject player2Bottom;

	// velocity reset
	private int velocityReset;

	//Received ball positions
	public static float receivedBallX;
	public static float receivedBallY;
	public static float receivedBallVel;



	// Use this for initialization
	void Start () { 
	}

	void Update()
	{
		changeBallColor ();
		//________SEND POSITIONS TO SERVER HERE_________
		ballPosition = transform.position;

		//Receiving ball positions from server
		receivedBallX = ((GameProcess.ballPosX/ Screen.width) * 12) - 6;
		receivedBallY = ((GameProcess.ballPosY/ Screen.height) * 12) - 6;
		receivedBallVel = GameProcess.ballVel;

		ballPosition = new Vector3(receivedBallX, receivedBallY, 0);


	}

	public void BallStart()
	{
		var randomNumber = Random.Range(0,2);

		//if collides with player 1, go positive constant direction
		if(randomNumber <= 0.5)
		{
			rigidbody2D.AddForce(new Vector2 (ballSpeed, 10));
		}
		else
		{
			//if collides with player 2, go negative constant directioin
			rigidbody2D.AddForce(new Vector2 (-ballSpeed, -10));
		}
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

		if (y > 0.5)
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Mathf.Abs(rigidbody2D.velocity.y));
		}
		else if (y < -0.5)
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -Mathf.Abs(rigidbody2D.velocity.y));
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




