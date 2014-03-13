using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour {
	float limit = 4.45f;

	private float speed = 8.0f;


	public static float player2PosX; 
	public static float player2PosY;
	//public static float player1PosZ;
	float lastY = 128;

	float deltaPosition;




	private GameProcess gp;

	public GameObject player1;


	// Use this for initialization
	void Start () {
	
		gp = GameObject.Find("_GameManager").GetComponent<GameProcess>();

	}
	
	// Update is called once per frame
	void Update () {
		//float y = ((Input.mousePosition.y / Screen.height) * 12) - 6;


		float transAmount = speed * Time.deltaTime;

		 
		//if (y > limit)
		//	y = limit;
		//else if (y < -limit)
		//	y = -limit;


		//if(GameProcess.player == 1)
		//{
			////float temp1 = (float)GameProcess.opPosY + GameObject.Find ("BottomWall").transform.position.y;
			////float wallRatio = (250.0f / GameObject.Find ("TopWall").transform.position.y - GameObject.Find ("BottomWall").transform.position.y);
			////float result = (float)(temp1 / wallRatio);//Convert.ToInt32(temp1 * wallRatio);
			//Debug.Log("opponent position: " + GameProcess.opPosY);
			//float oppY = ((GameProcess.opPosY / Screen.height) * 12) - 6;
			//transform.position = new Vector3(8, oppY, 0);
		//}



		//if(Client.playerThatClientControls == 2)
		//if(Client.playerThatClientControls == 1)
		//if(GameProcess.buffer == 1)
		if(GameProcess.player == 2)
		{
			//transform.position = new Vector3 (8, y, 0);
			if(Input.GetKey(KeyCode.UpArrow))
			{
				transform.Translate (0,transAmount,0);
			}
			
			if(Input.GetKey (KeyCode.DownArrow))
			{
				transform.Translate (0,-transAmount,0);
			}



			player2PosX = transform.position.x;
			player2PosY = transform.position.y;

			//deltaPosition = y - lastY;
			deltaPosition = transAmount - lastY;

//			Debug.Log("current possition: " + y);
//			Debug.Log("last position: " + lastY);
//			Debug.Log("delta position: " + deltaPosition);
			
			if(deltaPosition > .04)
			{
				gp.sendPositions ();
				gp.sendPositions ();
				gp.sendPositions ();
				gp.sendPositions ();
			}

		}
		else
		{
			
			//float temp1 = (float)GameProcess.opPosY + GameObject.Find ("BottomWall").transform.position.y;
			//float wallRatio = (250.0f / GameObject.Find ("TopWall").transform.position.y - GameObject.Find ("BottomWall").transform.position.y);
			//float result = (float)(temp1 / wallRatio);//Convert.ToInt32(temp1 * wallRatio);
			Debug.Log("opponent position: " + GameProcess.opPosY);
			float oppY = ((GameProcess.opPosY / Screen.height) * 12) - 6;
			transform.position = new Vector3(8, oppY, 0);
		}




		//lastY = y;
		lastY = transAmount;

	}
}
