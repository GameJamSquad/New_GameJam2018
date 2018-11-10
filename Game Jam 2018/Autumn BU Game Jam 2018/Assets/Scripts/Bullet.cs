using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public float bulletForce = 16f;

    public int damage = 1;

    public TankManager tSource;

	void Start ()
    {
        StartCoroutine(BulletCountDown());
	}

	void Update ()
    {
        rb.AddForce(transform.forward * bulletForce);
    }

    IEnumerator BulletCountDown()
    {
        yield return new WaitForSeconds(20f);
        ExplodeBullet();
    }

    void ExplodeBullet()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            TankManager tHit = other.transform.parent.GetComponent<TankManager>();
            tHit.health -= damage;
            if (tHit.health <= 0)
            {
                tSource.AdjustScore();
            }

            ExplodeBullet();
        }
        else if(other.gameObject.tag == "Turret")
        {
            TankManager tHit = other.gameObject.GetComponent<TurretHitDetection>().tankObject.GetComponent<TankManager>();
            tHit.health -= damage;

            if(tHit.health <= 0)
            {
                tSource.AdjustScore();
            }

            ExplodeBullet();
        }
        else
        {
            ExplodeBullet();
        }
    }
}
