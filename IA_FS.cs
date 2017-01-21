using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_FS : MonoBehaviour {

    public float range;
    public Transform target;
    public float speed;
    public float jump = 2;
    public Rigidbody2D rb;

    private Vector2 movement;
    private Vector3 jumping = new Vector3(0, 1,0);
    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody2D>();
        print(rb);
    }
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        movement = Vector2.MoveTowards(transform.position, target.position, step);
        //movement.y = 0;
        //transform.position = movement;
        //movement.y = 0;
        //print("movement x = " + (movement.x - transform.position.x));
        transform.Translate(movement.x-transform.position.x,0,0);

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            //transform.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
            print("should jump");
            //rb.AddForce(Vector3.up * jump);
            rb.AddForce(Vector2.up*jump, ForceMode2D.Impulse);
        }
    }

}
