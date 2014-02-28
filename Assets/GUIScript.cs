using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	private GameProcess process;

	// Use this for initialization
	void Start () {
		process = GameObject.Find("_GameManager").GetComponent<GameProcess>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,100,90), "Main Menu");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), "Connect")) 
		{

			//guiText.text = "Connecting...";
			if (process.returnSocket().Connect() )
			{	
				Debug.Log("Hi");
				//show = !show;
				//guiText.text = "Connect Succeeded";

				
				
			}
			//else guiText.text = "Connect Failed";
		
		}

		GUI.Box (new Rect (10,Screen.height -290,100,90), "Start Game");

		if(GUI.Button(new Rect(20,Screen.height - 260, 80, 20), "Send"))
		{
			//Application.loadedLevel(1);
		}



	}
}
