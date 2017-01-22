using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class Player : MonoBehaviour
{

    public Transform spawn;
    public int pointLife = 10;
    public int maxLife = 100;
    public bool isAlive = true;


    Camera mainCamera;
    private bool printDead = true;
	public float deathHeight = -30;

	public bool isFrozen = false;

    private AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip hurtClip;
    public AudioClip deathClip;
    public AudioClip respawnClip;


    void Start()
    {
        this.transform.position = spawn.transform.position;
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
		if (GetComponent<Transform> ().position.y < deathHeight) {
			pointLife = 0;
		}
		if (pointLife <= 0) { isAlive = false; } else { isAlive = true; }

        if (isAlive == false && printDead == true)
        {
            audioSource.PlayOneShot(deathClip);
			Freeze (true);
            printDead = false;
			StartCoroutine (respawn ());
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ennemy" && isAlive == true)
        {
            getDamage(10);
            //print("took damages !");
        }

    }

    public void getDamage(int damages)
    {
        this.pointLife -= damages;
        audioSource.PlayOneShot(hurtClip);
    }

	public void Freeze(bool value) {
		GetComponent<Platformer2DUserControl>().enabled = !value;
		isFrozen = value;
		GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
		Animator animator = GetComponent<Animator> ();
		animator.SetBool ("Frozen", value);
		animator.Play ("Idle");
	}

	IEnumerator respawn() {
		yield return new WaitForSeconds(2);
		GetComponent<Transform> ().position = spawn.position;
		pointLife = maxLife;
		isAlive = true;
		printDead = true;
		Freeze (false);
        audioSource.PlayOneShot(respawnClip);
	}
}
