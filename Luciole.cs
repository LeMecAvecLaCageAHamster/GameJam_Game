﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Character bound to the player spreading light
 * Dependancies:
 *   - RigidBody2D
 *   - CircleCollider
 */
public class Luciole : MonoBehaviour {
	private Rigidbody2D rb;

	public Transform player;
	private bool attached = true;
	public Light light;

	public float maxSpeed = 5f;
	public float acceleration = 9f;
	public float deceleration = 0.9f;

	bool flag = true; 

	private Player hero; 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
		Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>());
		hero = player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!attached) {
			Move ();
		} else if (Vector3.Distance (player.position, GetComponent<Transform> ().position) > 0.5f) {
			GoBackToPlayer ();
		} else if (hero.isFrozen) {
			rb.velocity = new Vector3 (0, 0, 0);
			rb.MovePosition (player.position);
			hero.Freeze (false);
			Debug.Log ("hate ya");
		}

		if (Input.GetButtonDown("Fire1")) {
			attached = !attached;
			GetComponent<CircleCollider2D> ().enabled = !attached;
			if (attached) {
				rb.velocity = new Vector2 (0f, 0f);
			} else {
				hero.Freeze (true);
			}
		}

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

	void Move() {
		float hMovement = Input.GetAxis ("Horizontal") != 0f ? 
			Input.GetAxis ("Horizontal") * rb.velocity.x >= 0 ?
				Input.GetAxis ("Horizontal") * acceleration :
				Input.GetAxis ("Horizontal") * 10*acceleration :
			0;
		float vMovement = Input.GetAxis ("Vertical") != 0f ? 
			Input.GetAxis ("Vertical") * rb.velocity.y >= 0 ?
				Input.GetAxis ("Vertical") * acceleration :
				Input.GetAxis ("Vertical") * 10*acceleration :
			0;

		Vector2 moveDirection = new Vector3 (
			Mathf.Abs(rb.velocity.x) <= maxSpeed || rb.velocity.x * hMovement < 0 ? hMovement : 0f,
			Mathf.Abs(rb.velocity.y) <= maxSpeed || rb.velocity.y * vMovement < 0 ? vMovement : 0f);
		rb.AddForce (moveDirection);

		if (Mathf.Abs(Input.GetAxis ("Horizontal")) < 1f && Mathf.Abs(Input.GetAxis ("Vertical")) < 1f) {
			rb.velocity = rb.velocity * deceleration;
		}
	}

	void GoBackToPlayer() {
		Vector3 direction = (player.position - GetComponent<Transform>().position).normalized;
		rb.MovePosition(transform.position + direction * acceleration * 4 * Time.deltaTime);
	}

	public IEnumerator Wait(float time) 
	{ 

		yield return new WaitForSeconds(time); 
		hero.pointLife ++; 
		flag = true; 
	}
}
