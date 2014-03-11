using UnityEngine;
using System.Collections;

public class CameraAdjust : MonoBehaviour {
	public Camera mainCamera;

	public BoxCollider2D topBorder;
	public BoxCollider2D bottomBorder;
	public BoxCollider2D leftBorder;
	public BoxCollider2D rightBorder;

	// Use this for initialization
	void Start () {
		topBorder.size = new Vector2(mainCamera.ScreenToWorldPoint (new Vector3(Screen.width * 2f,0f, 0f)).x, 1f);
		topBorder.center = new Vector2 (0f, mainCamera.ScreenToWorldPoint (new Vector3 (0f, Screen.height, 0f)).y + .5f);

		bottomBorder.size = new Vector2(mainCamera.ScreenToWorldPoint (new Vector3(Screen.width * 2f,0f, 0f)).x, 1f);
		bottomBorder.center = new Vector2 (0f, mainCamera.ScreenToWorldPoint (new Vector3 (0f, Screen.height, 0f)).y - .5f);

		leftBorder.size = new Vector2(1f, mainCamera.ScreenToWorldPoint (new Vector3(0f, Screen.height * 2f,0f)).y);
		leftBorder.center = new Vector2 (mainCamera.ScreenToWorldPoint (new Vector3 (0f, 0f, 0f)).x +.5f, 0f);

		rightBorder.size = new Vector2(1f, mainCamera.ScreenToWorldPoint (new Vector3(0f, Screen.height * 2f,0f)).y);
		rightBorder.center = new Vector2 (mainCamera.ScreenToWorldPoint (new Vector3 (Screen.width, 0f, 0f)).x +.5f, 0f);


		                                                                                                                
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
