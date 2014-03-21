using UnityEngine;
using System.Collections;
using System;


public class Player1 : MonoBehaviour {
	float limit = 4.45f;

	private float speed = 8.0f;

	public static float player1PosX; 
	public static float player1PosY;
	//public static float player1PosZ;

	float deltaPosition;

	private GameProcess gp;
	private float lastY;

	public GameObject player1;

	// Use this for initialization
	void Start () {
	
		gp = GameObject.Find("_GameManager").GetComponent<GameProcess>();
		lastY = 128;
		transform.position = new Vector3 (-8, 0, 0);


	}
	
	// Update is called once per frame
	void Update () {

		// current position
		//float y = ((Input.mousePosition.y / Screen.height) * 12) - 6;

		float transAmount = speed * Time.deltaTime;

		//if (y > limit)
		//	y = limit;
		//else if (y < -limit)
		//	y = -limit;



		//if(GameProcess.player == 2)
		//{

			////float temp1 = (float)GameProcess.opPosY + GameObject.Find ("BottomWall").transform.position.y;
			////float wallRatio = (250.0f / GameObject.Find ("TopWall").transform.position.y - GameObject.Find ("BottomWall").transform.position.y);
			////float result = (float)(temp1 / wallRatio);//Convert.ToInt32(temp1 * wallRatio);
			//Debug.Log("opponent position: " + GameProcess.opPosY);
			//float oppY = ((GameProcess.opPosY / Screen.height) * 12) - 6;
			//transform.position = new Vector3(-8, oppY, 0);
		//}


		//if(Client.playerThatClientControls == 1)
		//if(GameProcess.buffer == 1)
		if(GameProcess.player == 1)
		{
			if(Input.GetKey(KeyCode.UpArrow))
			{
				transform.Translate (0,transAmount,0);
			}

			if(Input.GetKey (KeyCode.DownArrow))
			{
				transform.Translate (0,-transAmount,0);
			}

			//transform.position = new Vector3 (-8, y, 0);
			player1PosX = transform.position.x;
			player1PosY = transform.position.y;
			//deltaPosition = y - lastY;
			deltaPosition = transAmount - lastY;
//			Debug.Log("current possition: " + y);
//			Debug.Log("last position: " + lastY);
//			//Debug.Log("delta position: " + deltaPosition);
			//
//			if(deltaPosition > .04)
//			{
//				gp.sendPositions ();
//				gp.sendPositions ();
//				gp.sendPositions ();
//				gp.sendPositions ();
//			}
//			
			if(GameProcess.ballPosX != 0)
				gp.sendPositions();
		}
		else
		{
			
			//float temp1 = (float)GameProcess.opPosY + GameObject.Find ("BottomWall").transform.position.y;
			//float wallRatio = (250.0f / GameObject.Find ("TopWall").transform.position.y - GameObject.Find ("BottomWall").transform.position.y);
			//float result = (float)(temp1 / wallRatio);//Convert.ToInt32(temp1 * wallRatio);
//			Debug.Log("opponent position: " + GameProcess.opPosY);

            //

			if(GameProcess.opPosY != 0)
			{
				float oppY = (GameProcess.opPosY / GameProcess.wallRatio) + gp.bWall.transform.position.y;
				transform.position = new Vector3(-8, oppY, 0);
			}

		}

		//lastY = y;
		lastY = transAmount;

	}
}
