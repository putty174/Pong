using UnityEngine;
using System.Collections;

public class GameProcess : MonoBehaviour {

	private GameObject p1;
	private GameObject p2;
	private GameObject ball;

	//PRIVATE MEMBERS
	private Sockets sockets;
	private Client client;
	private GUIScript gui;

	private byte[] buffer;

	// Use this for initialization
	void Start () {

		sockets = new Sockets();
		client = new Client();

		gui = GameObject.Find("GUI").GetComponent<GUIScript>();

		p1 = GameObject.Find ("Player1");
		p2 = GameObject.Find ("Player2");
	}
	
	// Update is called once per frame
	void Update () {
		client.Send ("Player 1: " + p1.transform.position);
		client.Send ("Player 2: " + p2.transform.position);
		if(client.receiverBuffer.Count > 0)
		{
			lock(client.receiverBuffer)
			{
				while(client.receiverBuffer.Count > 0)
				{
					buffer = (byte[]) client.receiverBuffer.Dequeue();
					Debug.Log(System.Text.Encoding.ASCII.GetString(buffer));
				}
			}
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
