using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	GUISkin skin;
	public Texture2D logoTexture;

	void OnGUI () {
		GUI.Label (new Rect (0,0,100,50), logoTexture);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
