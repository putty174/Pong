using UnityEngine;
using System.Collections;

public class Player1 : MonoBehaviour {
	float limit = 4.45f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float y = ((Input.mousePosition.y / Screen.height) * 12) - 6;

		if (y > limit)
			y = limit;
		else if (y < -limit)
			y = -limit;


		if(Client.playerThatClientControls == 1)
			transform.position = new Vector3 (-8, y, 0);
	}
}
