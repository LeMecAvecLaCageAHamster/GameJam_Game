﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Character bound to the player spreading light
 * Dependancies:
 *   - RigidBody2D
 *   - CircleCollider
 *   - Hero (Player)
 *   - wave (circular sprite)
 */
public class Luciole : MonoBehaviour {
	private Rigidbody2D rb;

	public Transform player;
	private bool attached = true;
	public Light light;
    public float range = 5;

	public float acceleration = 9f;
	public float deceleration = 0.9f;
	public float distancePlayer = 1.5f;
	public float backSpeed = 30;

	public float waveRange = 5f;
	public float waveSpeed = 0.5f;
	public int shockwaveCost = 10;
	public Transform wave;

    private AudioSource audioSource;
    public AudioClip detachClip;
    public AudioClip attachClip;
    public AudioClip refillClip;
    public AudioClip waveClip;
    public AudioClip emptyEnergyClip;


	bool flag = true; 
	bool respawn = true;

	private Player hero; 

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
		Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>());
		hero = player.GetComponent<Player>();

        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		if (!attached) {
			Move ();
			Attack ();
		} else if (Vector3.Distance (player.position, GetComponent<Transform> ().position) > distancePlayer) {
			GoBackToPlayer ();
		} else if (hero.isFrozen && hero.isAlive) {
			rb.velocity = new Vector3 (0, 0, 0);
			rb.MovePosition (player.position);
			hero.Freeze (false);
		} else if (GetComponent<Transform> ().position.x != player.position.x || GetComponent<Transform> ().position.y != player.position.y) {
			GetComponent<Transform> ().position = player.position;
		}

		if (Input.GetButtonDown("Fire1")) {
			attached = !attached;
			GetComponent<CircleCollider2D> ().enabled = !attached;
			if (attached) {
				rb.velocity = new Vector2 (0f, 0f);
                audioSource.PlayOneShot(attachClip);
			} else {
				hero.Freeze (true);
				rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                audioSource.PlayOneShot(detachClip);
			}
		}

		if (!hero.isAlive) {
			if (!attached) {
				attached = true;
				GetComponent<CircleCollider2D> ().enabled = false;
			}
			GetComponent<Transform> ().position = player.position;
			respawn = true;
		}
		if (respawn && hero.isAlive) {
			GetComponent<Transform> ().position = player.position;
			respawn = false;
		}

		//light.range = ((float)hero.pointLife) * Time.deltaTime * 20; //ARCHI VERY ULTRA TROP IMPORTANT MAGGLE !!!!!!!!!!!!!!! 

		//light.range = ((float)hero.pointLife) / 5;

		light.range = ((float)hero.pointLife) /  range;

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
			Input.GetAxis ("Horizontal") * acceleration :
			rb.velocity.x;
		float vMovement = Input.GetAxis ("Vertical") != 0f ? 
			Input.GetAxis ("Vertical") * acceleration :
			rb.velocity.y;

		Vector2 moveDirection = new Vector3 (hMovement, vMovement);
		rb.velocity = moveDirection;

		if (Mathf.Abs(Input.GetAxis ("Horizontal")) < 1f && Mathf.Abs(Input.GetAxis ("Vertical")) < 1f) {
			rb.velocity = rb.velocity * deceleration;
		}
	}

	void Attack() {
		if (Input.GetButtonDown ("Fire2") && hero.pointLife > shockwaveCost) {
            audioSource.PlayOneShot(waveClip);
			ShockWave ();
		}
        else
        {
            audioSource.PlayOneShot(emptyEnergyClip);
        }
	}

	void ShockWave() {
		Collider2D[] hits = Physics2D.OverlapCircleAll (
			                   GetComponent<Transform> ().position,
			                   waveRange);
		foreach (Collider2D hit in hits) {
			Monster monster = hit.transform.GetComponent<Monster> ();
			if (monster != null) {
				monster.Trigger (GetComponent<Transform> ().position);
			}
		}

		hero.getDamage (shockwaveCost);

		StartCoroutine (AnimateWave ());
	}

	void GoBackToPlayer() {
		Vector3 direction = (player.position - GetComponent<Transform>().position).normalized;
		rb.MovePosition(transform.position + direction * backSpeed  * Time.deltaTime);
	}

	public IEnumerator Wait(float time) 
	{ 

		yield return new WaitForSeconds(time); 
		hero.pointLife ++; 
		flag = true; 
	}

	IEnumerator AnimateWave() {
		float alpha = 1f;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / waveSpeed)
		{
			// Opacity
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha , 0f, t));
			wave.GetComponent<Renderer>().material.color = newColor;

			// Size
			wave.localScale = Vector3.Lerp(new Vector3(0, 0, 1), new Vector3(waveRange * 2, waveRange * 2, 1), t);
			yield return null;
		}
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "energy")
        {
            print("near source of energy");
            if (hero.pointLife < hero.maxLife)
            {
                // hero.pointLife = hero.maxLife;
                audioSource.PlayOneShot(refillClip);
                StartCoroutine(AnimatedLife());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ennemy" && hero.isAlive == true)
        {
            hero.getDamage(20);
        }

    }

    public IEnumerator AnimatedLife()
    {
        float startLife = hero.pointLife;
        float endLife = hero.maxLife;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 2f)
        {
            hero.pointLife = Mathf.RoundToInt(Mathf.Lerp(startLife, endLife, t));
            print(hero.pointLife);
            yield return null;
        }

        hero.pointLife = hero.maxLife;

        print("Now full life !");

        
    }

}
