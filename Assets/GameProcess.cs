using UnityEngine;
using System.Collections;

public class GameProcess : MonoBehaviour {

	//PRIVATE MEMBERS
	private Sockets sockets;
	private GUIScript gui;

	// Use this for initialization
	void Start () {

		sockets = new Sockets();

		gui = GameObject.Find("GUI").GetComponent<GUIScript>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Sockets returnSocket ()
	{
		return sockets;
	}
}
