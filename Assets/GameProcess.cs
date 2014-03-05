using UnityEngine;
using System.Collections;

public class GameProcess : MonoBehaviour {

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
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space"))
		{
			client.Send ("HA");
		}
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
