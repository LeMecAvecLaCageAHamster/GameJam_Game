using UnityEngine;
using System.Collections;

public class player : MonoBehaviour
{

    public Transform spawn;
    public double pointLife = 100;
    public bool isAlive = true;

    void Start()
    {
        this.transform.position = spawn.transform.position;
    }


    void Update()
    {

        if (pointLife <= 0) { isAlive = false; } else { isAlive = true; }
    }

    void getDamage(double damages)
    {
        this.pointLife -= damages;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ennemy")
        {
            getDamage(25);
            print("took damages !");
        }
    }

}
