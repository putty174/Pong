#pragma strict

var mainCamera : Camera;

var topBoundary : BoxCollider2D;
var bottomBoundary : BoxCollider2D;
var leftBoundary : BoxCollider2D;
var rightBoundary : BoxCollider2D;

var player1 : Transform;
var player2 : Transform;
function Start () {

}

function Update () {
	// move each wall to its edge location
	topBoundary.size = new Vector2(mainCamera.ScreenToWorldPoint (new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
	topBoundary.center = new Vector2 (0f, mainCamera.ScreenToWorldPoint (new Vector3(0f, Screen.height, 0f)).y + 0.5f);
	
	bottomBoundary.size = new Vector2(mainCamera.ScreenToWorldPoint (new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
	bottomBoundary.center = new Vector2 (0f, mainCamera.ScreenToWorldPoint (new Vector3(0f, Screen.height, 0f)).y + 0.5f);
	
	leftBoundary.size = new Vector2(mainCamera.ScreenToWorldPoint (new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
	leftBoundary.center = new Vector2 (0f, mainCamera.ScreenToWorldPoint (new Vector3(0f, Screen.height, 0f)).y + 0.5f);
	
	rightBoundary.size = new Vector2(mainCamera.ScreenToWorldPoint (new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
	rightBoundary.center = new Vector2 (0f, mainCamera.ScreenToWorldPoint (new Vector3(0f, Screen.height, 0f)).y + 0.5f);
}