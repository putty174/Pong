using UnityEngine;
using System.Collections;



public class PlayerScript : MonoBehaviour {
	
	
	public KeyCode up;
	public KeyCode down;
	public float paddleSpeed = .1f;
	public float paddlePosition = 12.4f/32f;
	public float acceleration = 30f;
	
	private float input;
	
	private bool isStopped = false;
	
	private GameObject playerToControl;

	public GameObject player1;
	public GameObject player2;

	
	// Use this for initialization
	void Start () {
		
		if(Client.playerThatClientControls == 1)
		{
			//playerToControl = GameObject.Find("Player1");
			playerToControl = player1;
		}
		else //if playerThatClientControls == 2
		{
			//playerToControl = GameObject.Find ("Player2");
			playerToControl = player2;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//Vector2 temp = rigidbody2D.velocity;
		
		// get the current position
		//Vector2 pos = transform.position;
		
		
		Vector2 temp = playerToControl.rigidbody2D.velocity;
		
		// get the current position
		Vector2 pos = playerToControl.transform.position;
		
		
		
		
		if (Input.GetKey(up)) 
		{
			// player wants to move the racket upwards
			//rigidbody2D.velocity = new Vector2(0, paddleSpeed);
			playerToControl.rigidbody2D.velocity = new Vector2(0, paddleSpeed);
		} 
		else if (Input.GetKey(down)) 
		{                       
			// player wants to move the racket downwards
			//rigidbody2D.velocity = new Vector2(0, paddleSpeed *-1);
			playerToControl.rigidbody2D.velocity = new Vector2(0, paddleSpeed *-1);
		}
		else
		{
			//rigidbody2D.velocity = new Vector2(0,0);
			playerToControl.rigidbody2D.velocity = new Vector2(0,0);
		}
		
		
		
		
		
		
		
	}
	
	public void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "Wall") 
		{
			Debug.Log("hi");
			isStopped = true;
		}
	}
	
	
	
	
	
	
}
