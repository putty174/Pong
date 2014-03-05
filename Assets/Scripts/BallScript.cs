using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

	// Start speed of the ball
	public float ballSpeed = 100;

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

	// velocity
	//private int velocityY = rigidbody2D.velocity.y;



	// velocity reset
	private int velocityReset;



	// Use this for initialization
	void Start () {

		Invoke("BallStart", 3);
		







	
	}

	public void BallStart()
	{

		var randomNumber = Random.Range(0,2);
		if(randomNumber <= 0.5)
		{
			rigidbody2D.AddForce(new Vector2 (ballSpeed, 10));
		}
		else
		{
			rigidbody2D.AddForce(new Vector2 (-ballSpeed, -10));
		}
	}

	public void BallReset()
	{
		rigidbody2D.velocity = new Vector2(0,0);



		transform.position = Vector3.zero;

		Invoke("BallStart", .5f);
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




