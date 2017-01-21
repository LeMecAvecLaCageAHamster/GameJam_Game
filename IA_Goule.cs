using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Goule : MonoBehaviour {

    //Public values
    public float range = 0.5f;
    public Transform LIAL;
    public Transform LIAR;
    private float sensDeplacement = -0.02f;
    

    // Use this for initialization
    void Start () {
        
    }


    // Update is called once per frame
    void Update () {
        if (sensDeplacement < 0 && Vector3.Distance(this.transform.position,LIAL.position ) < range){
            sensDeplacement = 0.02f;
        }else if (sensDeplacement > 0 && Vector3.Distance(this.transform.position, LIAR.position) < range){
            sensDeplacement = -0.02f;
        }

        transform.Translate(sensDeplacement,0,0);
    }
}
