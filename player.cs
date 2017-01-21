using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class Player : MonoBehaviour
{

    public Transform spawn;
    public int pointLife = 100;
    public bool isAlive = true;
    Camera mainCamera;

	public bool isFrozen = false;

    void Start()
    {
        this.transform.position = spawn.transform.position;
    }


    void Update()
    {
        if (pointLife <= 0) { isAlive = false; } else { isAlive = true; }

        if (isAlive == false)
        {
            this.transform.DetachChildren();
            
        }
    }

    void getDamage(int damages)
    {
        this.pointLife -= damages;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ennemy")
        {
            getDamage(10);
            //print("took damages !");
        }
    }

    void deadParticles()
    {

    }

	public void Freeze(bool value) {
		GetComponent<Platformer2DUserControl>().enabled = !value;
		isFrozen = value;
	}
}
