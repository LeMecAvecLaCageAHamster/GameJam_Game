using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour {
	private Vector3 center;
	public float radius = 1;
	public float delay = 2;
	private bool moving = false;
	private bool flip = true;

	// Use this for initialization
	void Start () {
		center = GetComponent<Transform> ().localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (!moving) {
			Vector3 goal = new Vector3 (Random.Range (center.x - radius, center.x + radius), Random.Range (center.y - radius, center.y + radius), transform.position.z);
			moving = true;
			StartCoroutine (MoveToGoal(goal));
		}
		if (flip) {
			Vector3 theScale = transform.localScale;
			theScale.x = - theScale.x;
			transform.localScale = theScale;
			flip = false;
			Debug.Log ("Flip bru");
			StartCoroutine (WaitBeforeFlip ());
		}
	}

	IEnumerator MoveToGoal(Vector3 goal) {
		Vector2 start = GetComponent<Transform> ().localPosition;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / delay)
		{
			float posX = Mathf.Lerp (start.x, goal.x, t);
			float posY = Mathf.Lerp (start.y, goal.y, t);
			GetComponent<Transform> ().localPosition = new Vector3 (posX, posY, transform.position.z);

			yield return null;
		}
		moving = false;
	}

	IEnumerator WaitBeforeFlip() {
		yield return new WaitForSeconds (Random.Range (2, 6));
		Debug.Log ("next");
		flip = true;
	}
}
