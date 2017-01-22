﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_FS : Monster {

    public float range;
    public Transform target;
    public float speed;
    public float jump = 2;
    public Rigidbody2D rb;
    public Luciole luciole;
    public bool isTriggered = false;
    public int pv = 3;

    private Vector2 movement;
    private Vector3 jumping = new Vector3(0, 1,0);
    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody2D>();
        print(rb);
    }
	
    public override void Trigger(Vector2 source)
    {
        isTriggered = true;
        if (pv > 0) {
            pv--;
            print("pv : " + pv);
                }
        if (pv == 0) Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        //if (Vector2.Distance(transform.position, luciole.transform.position) < luciole.light.range + range) isTriggered = true;


        if (isTriggered)
        {
            movement = Vector2.MoveTowards(transform.position, target.position, step);
            transform.Translate(movement.x - transform.position.x, 0, 0);
        }

    }

	void FixedUpdate() {
	// Set the vertical animation
		Animator m_Anim = GetComponent<Animator>();
		m_Anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "platform")
        {
            //transform.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
            print("should jump");
            //rb.AddForce(Vector3.up * jump);
            rb.AddForce(Vector2.up*jump, ForceMode2D.Impulse);
        }
    }

}
