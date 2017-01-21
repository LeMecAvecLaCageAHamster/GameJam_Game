using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Luciole : MonoBehaviour {
	private Rigidbody2D rb;

	static public Transform player;
	public bool attached = true;
    public Light light;

	public float maxSpeed = 5f;
	public float acceleration = 9f;
	public float deceleration = 0.9f;

    bool flag = true;

    public Player hero = player.GetComponent<Player>();

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
        /*float hMovement = Input.GetAxis ("Horizontal") != 0f ? 
           Input.GetAxis ("Horizontal") * rb.velocity.x >= 0 ?
               Input.GetAxis ("Horizontal") * acceleration :
               Input.GetAxis ("Horizontal") * 10*acceleration :
           //deceleration * (rb.velocity.x != 0 ? rb.velocity.x > 0 ? -1 : 1 : 0);
           0;
       float vMovement = Input.GetAxis ("Vertical") != 0f ? 
           Input.GetAxis ("Vertical") * rb.velocity.y >= 0 ?
               Input.GetAxis ("Vertical") * acceleration :
               Input.GetAxis ("Vertical") * 10*acceleration :
           //deceleration * (rb.velocity.y != 0 ? rb.velocity.y > 0 ? -1 : 1 : 0);
           0;

       Vector2 moveDirection = new Vector3 (
           Mathf.Abs(rb.velocity.x) <= maxSpeed || rb.velocity.x * hMovement < 0 ? hMovement : 0f,
           Mathf.Abs(rb.velocity.y) <= maxSpeed || rb.velocity.y * vMovement < 0 ? vMovement : 0f);
       rb.AddForce (moveDirection);

       if (Mathf.Abs(Input.GetAxis ("Horizontal")) < 1f && Mathf.Abs(Input.GetAxis ("Vertical")) < 1f) {
           rb.velocity = rb.velocity * deceleration;
       }*/


        //light.range = ((float)hero.pointLife) * Time.deltaTime * 20; //ARCHI VERY ULTRA TROP IMPORTANT MAGGLE !!!!!!!!!!!!!!!
        light.range = ((float)hero.pointLife) / 5;
        //light.color -= (Color.cyan) * (2.0F * Time.deltaTime);

        while(hero.pointLife < 100 && hero.pointLife < 0)
        {
            if (flag == true)
            {
                hero.pointLife++;
                flag = false;
            }

        }


    }

    public IEnumerator Wait(float time)
    {

        yield return new WaitForSeconds(time);
        hero.pointLife ++;
        flag = true;

    }


}
