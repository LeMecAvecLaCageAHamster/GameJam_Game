using UnityEngine;
using System.Collections;

public class Stroboscop : MonoBehaviour {

    public Light light;
	
	void Update () {
        light.range = Random.Range(5 ,20);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            StartCoroutine(AnimatedLight());
        }
    }

    public IEnumerator AnimatedLight()
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 2f)
        {
            light.range = Mathf.RoundToInt(Mathf.Lerp(10, 0, t));
            yield return null;
        }

        Destroy(light.gameObject);
   
    }
}
