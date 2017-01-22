using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour {
	private Vector2 center;
	public float radius = 1;
	public float delay = 2;
	private bool moving = false;

	// Use this for initialization
	void Start () {
		center = GetComponent<Transform> ().localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (!moving) {
			Vector2 goal = new Vector2 (Random.Range (center.x - radius, center.x + radius), Random.Range (center.y - radius, center.y + radius));
			moving = true;
			StartCoroutine (MoveToGoal(goal));
		}
	}

	IEnumerator MoveToGoal(Vector2 goal) {
		Vector2 start = GetComponent<Transform> ().localPosition;
		Debug.Log ("start " + start.x);
		Debug.Log ("to " + goal.x);
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / delay)
		{
			float posX = Mathf.Lerp (start.x, goal.x, t);
			float posY = Mathf.Lerp (start.y, goal.y, t);
			GetComponent<Transform> ().localPosition = new Vector2 (posX, posY);

			yield return null;
		}
		Debug.Log ("End " + GetComponent<Transform> ().localPosition.x);

		moving = false;
	}
}
