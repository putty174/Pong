using UnityEngine;
using System.Collections;

public class GameProcess : MonoBehaviour {

	//PRIVATE MEMBERS
	private Sockets sockets;
	private Client client;
	private GUIScript gui;

	// Use this for initialization
	void Start () {

		sockets = new Sockets();
		client = new Client();

		gui = GameObject.Find("GUI").GetComponent<GUIScript>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space"))
		{
			client.Send ("HA");
		}
	}

	public Sockets returnSocket ()
	{
		return sockets;
	}

	public Client returnClient()
	{
		return client;
	}
}
