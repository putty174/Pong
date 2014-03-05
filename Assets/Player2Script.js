#pragma strict

var up : KeyCode;
var down : KeyCode;

var speed : float = 10;


var thisClientControlsPlayer2 : int = 2;




function Start () {

}

function Update () {

//Client.cs's playerThatClientControls field must be called
//to determine whether or not player2 is controlled.  PLEASE convert this to C#


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
	
	
	//________SEND POSITIONS TO SERVER HERE_________//Done in Client.cs?
	
	
	
	
	
	
	
	
	
	
	
	
		

}