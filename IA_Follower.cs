using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Follower : MonoBehaviour {

    public float range;
    public Transform target;
    public float speed;
    public Luciole luciole;

    public bool isTriggered = false;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        // luciole.transform.position
        // luciole.light.range
        // transform.position
        if (Vector2.Distance(transform.position, luciole.transform.position)< luciole.light.range+range) isTriggered = true; 

        if (isTriggered)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

			// Flip
			Vector3 theScale = transform.localScale;
			theScale.x = (target.position.x < transform.position.x ? -1 : 1) * Mathf.Abs(theScale.x);
			transform.localScale = theScale;
        }
        


    }


}
