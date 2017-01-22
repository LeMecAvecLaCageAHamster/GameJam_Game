using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Goule : MonoBehaviour {

    //Public values
    public float speed = 0.1f;
    public float range = 0.5f;
    public Transform LIAL;
    public Transform LIAR;
    private float sensDeplacement = -2f;
    

    // Use this for initialization
    void Start () {
		Vector3 theScale = transform.localScale;
		theScale.x = -Mathf.Abs(theScale.x);
		transform.localScale = theScale;
    }


    // Update is called once per frame
    
    void Update () {
        float step = speed * Time.deltaTime;
		Vector3 theScale = transform.localScale;
        //float step = speed;
        if (sensDeplacement < 0 && Vector3.Distance(this.transform.position,LIAL.position ) < range){
            sensDeplacement = step;
			theScale.x = Mathf.Abs(theScale.x);
        }else if (sensDeplacement > 0 && Vector3.Distance(this.transform.position, LIAR.position) < range){
            sensDeplacement = 0-step;
			theScale.x = - Mathf.Abs(theScale.x);
        }
		transform.localScale = theScale;

        transform.Translate(sensDeplacement,0,0);
    }
}
