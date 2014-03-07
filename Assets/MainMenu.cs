using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUISkin skin;
	string title = "Pong2D";

	void OnGUI () {
		GUI.skin = skin;
		GUI.Label (new Rect (Screen.width/2 - 50, 20, 150,150), title);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
