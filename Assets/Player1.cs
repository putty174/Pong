using UnityEngine;
using System.Collections;


public class Player1 : MonoBehaviour {
	float limit = 4.45f;


	public static float player1PosX; 
	public static float player1PosY;
	//public static float player1PosZ;

	float deltaPosition;

	private GameProcess gp;
	private float lastY;


	// Use this for initialization
	void Start () {
	
		gp = GameObject.Find("_GameManager").GetComponent<GameProcess>();
		lastY = 128;



	}
	
	// Update is called once per frame
	void Update () {

		// current position
		float y = ((Input.mousePosition.y / Screen.height) * 12) - 6;



		if (y > limit)
			y = limit;
		else if (y < -limit)
			y = -limit;


		//if(Client.playerThatClientControls == 1)
		//if(GameProcess.buffer == 1)
		if(GameProcess.player == 1)
		{
			transform.position = new Vector3 (-8, y, 0);
			player1PosX = transform.position.x;
			player1PosY = transform.position.y;
			deltaPosition = y - lastY;
			Debug.Log("current possition: " + y);
			Debug.Log("last position: " + lastY);
			Debug.Log("delta position: " + deltaPosition);
			
			if(deltaPosition > .04)
			{
				gp.sendPositions ();
				gp.sendPositions ();
				gp.sendPositions ();
				gp.sendPositions ();
			}
				

		}

		lastY = y;

	}

}
