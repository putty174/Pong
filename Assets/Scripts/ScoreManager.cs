using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	// the score limit
	static public int scoreLimit = 10;


	// player 1's score
	// static to let us easily use this variable from any object form our scene
	static private int player1Score = 0;

	// player 2's score
	// static to let us easily use this variable from any object form our scene
	static private int player2Score = 0;


	public GUISkin skin;



	static public void ScoreCounter(string goal)
	{
		// player 1
		if(goal == "Goal1")
		{
			player2Score++;
		}

		// player 2 
		else if(goal == "Goal2")
		{
			player1Score++;
		}

		if(player1Score >= scoreLimit || player2Score >= scoreLimit)
		{
			// player 1 wins
			if(player1Score > player2Score)
			{
				Debug.Log("Player 1 wins!");
			}
			// player 2 wins
			if(player2Score > player1Score)
			{
				Debug.Log("Player 2 wins!");
			}
			else
			{
				Debug.Log("Tie Game!");
			}

			// reset scores
			player1Score = 0;
			player2Score = 0;
		}


	} 

	void OnGUI()
	{
		GUI.skin = skin;
		GUI.Box (new Rect (10,Screen.height -230,100,90), "Score");
		GUI.Label(new Rect(20,Screen.height -200, 100, 30), "Player1: " + player1Score);
		GUI.Label(new Rect(20,Screen.height -170, 100, 30), "Player2: " + player2Score);




	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
