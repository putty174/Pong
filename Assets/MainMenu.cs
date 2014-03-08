using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUISkin skin;
	string title = "Pong2D";

	void OnGUI () {
		GUI.color = Color.white;
		GUI.skin = skin;
		GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
		centeredStyle.alignment = TextAnchor.UpperCenter;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
