﻿using UnityEngine;
using System.Collections;


public class Player1 : MonoBehaviour {
	float limit = 4.45f;

	private float speed = 8.0f;

	public static float player1PosX; 
	public static float player1PosY;
	//public static float player1PosZ;

	float deltaPosition;

	private GameProcess gp;
	private float lastY;

<<<<<<< HEAD
	private int speed = 10;


	public GameObject player2;
=======
	public GameObject player1;
>>>>>>> FETCH_HEAD

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



		if(GameProcess.player == 2)
		{

			float temp1 = (float)GameProcess.opPosY + GameObject.Find ("BottomWall").transform.position.y;
			float wallRatio = (250.0f / GameObject.Find ("TopWall").transform.position.y - GameObject.Find ("BottomWall").transform.position.y);
			float result = (float)(temp1 / wallRatio);//Convert.ToInt32(temp1 * wallRatio);
			Debug.Log("opponent position: " + GameProcess.opPosY);
			float oppY = ((GameProcess.opPosY / Screen.height) * 12) - 6;
			transform.position = new Vector3(-8, oppY, 0);
		}



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
<<<<<<< HEAD
//			Debug.Log("my position: " + player1PosY);
			deltaPosition = y - lastY;
//			Debug.Log("current possition: " + y);
//			Debug.Log("last position: " + lastY);
//			Debug.Log("delta position: " + deltaPosition);
			

				gp.sendPositions ();
				gp.sendPositions ();
				gp.sendPositions ();
				gp.sendPositions ();

				

		}



		//lastY = y;
=======
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
			gp.sendPositions();
		}
		else
		{
			
			//float temp1 = (float)GameProcess.opPosY + GameObject.Find ("BottomWall").transform.position.y;
			//float wallRatio = (250.0f / GameObject.Find ("TopWall").transform.position.y - GameObject.Find ("BottomWall").transform.position.y);
			//float result = (float)(temp1 / wallRatio);//Convert.ToInt32(temp1 * wallRatio);
//			Debug.Log("opponent position: " + GameProcess.opPosY);
			float oppY = (GameProcess.opPosY / gp.wallRatio) + gp.bWall.transform.position.y;
			transform.position = new Vector3(-8, oppY, 0);
		}

		//lastY = y;
		lastY = transAmount;
>>>>>>> FETCH_HEAD

	}
}
