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
    public ParticleSystem particle;
    private bool printDead = true;


	public bool isFrozen = false;

    void Start()
    {
        this.transform.position = spawn.transform.position;
    }


    void Update()
    {
        if (pointLife <= 0) { isAlive = false; } else { isAlive = true; }

        if (isAlive == false && printDead == true)
        {
            this.transform.DetachChildren();
            particle.enableEmission = true;
            particle.Play();
            Destroy(this.gameObject);
            print("Instantie particules");
            printDead = false;
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
    }

	public void Freeze(bool value) {
		GetComponent<Platformer2DUserControl>().enabled = !value;
		isFrozen = value;
		GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
		Animator animator = GetComponent<Animator> ();
		animator.SetBool ("Frozen", value);
		animator.Play ("Idle");
	}

}
