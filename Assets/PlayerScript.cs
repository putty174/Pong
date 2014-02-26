using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float paddleSpeed = 15f;

	public float range = 5f;




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		float input = Input.GetAxis("Vertical");

		// move the paddle
		Vector2 pos = transform.position;
		pos.y += input * paddleSpeed * Time.deltaTime;

		pos.y = Mathf.Clamp(pos.y, -range, range);

		transform.position = pos;
	
	}


}
