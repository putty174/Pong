#pragma strict

var up : KeyCode;
var down : KeyCode;

var speed : float = 10;






function Start () {

}

function Update () {

	//if(Input.GetKey(up))
	if(Input.GetAxis("Mouse Y") > 0)
	{
		rigidbody2D.velocity.y = speed;
	}
	//else if(Input.GetKey(down))
	else if(Input.GetAxis("Mouse Y") < 0)
	{
		rigidbody2D.velocity.y = speed *-1;
	}
	else
	{
		rigidbody2D.velocity.y = 0;
	}
	
	rigidbody2D.velocity.x = 0;
		

}