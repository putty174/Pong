using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour {
	float limit = 4.45f;

	public static float player2PosX; 
	public static float player2PosY;
	//public static float player1PosZ;


	private GameProcess gp;


	// Use this for initialization
	void Start () {
	
		gp = GameObject.Find("_GameManager").GetComponent<GameProcess>();

	}
	
	// Update is called once per frame
	void Update () {
		float y = ((Input.mousePosition.y / Screen.height) * 12) - 6;
		
		if (y > limit)
			y = limit;
		else if (y < -limit)
			y = -limit;


		//if(Client.playerThatClientControls == 2)
		//if(Client.playerThatClientControls == 1)
		//if(GameProcess.buffer == 1)
		if(GameProcess.player == 2)
		{
			transform.position = new Vector3 (8, y, 0);
			player2PosX = transform.position.x;
			player2PosY = transform.position.y;

			gp.sendPositions ();
			gp.sendPositions ();
			gp.sendPositions ();
			gp.sendPositions ();
		}
	}
}
