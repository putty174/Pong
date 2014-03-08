using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D otherGameObject)
	{
		if(otherGameObject.name == "GameBall")
		{
			var goalName = transform.name;
			ScoreManager.ScoreCounter(goalName);
			otherGameObject.gameObject.SendMessage("BallReset");
		}
	}
}
