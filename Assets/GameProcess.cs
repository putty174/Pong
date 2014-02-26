using UnityEngine;
using System.Collections;

public class GameProcess : MonoBehaviour {

	//PRIVATE MEMBERS
	private Sockets sockets;

	// Use this for initialization
	void Start () {

		sockets = new Sockets();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Sockets returnSocket ()
	{
		return sockets;
	}
}
