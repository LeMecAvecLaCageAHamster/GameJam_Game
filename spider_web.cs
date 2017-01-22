using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider_web : MonoBehaviour {

    public IA_Follower mob;
    public float range = 0.01f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(this.transform.position, mob.transform.position) < range)
        {
            //mob.transform.RigidbodyConstraint = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            mob.speed = 0f;
        }
    }
}
