using UnityEngine;
using System.Collections;

public class Stroboscop : MonoBehaviour {

    public Light light;
	
	void Update () {
        light.range = Random.Range(5 ,20);
    }
}
