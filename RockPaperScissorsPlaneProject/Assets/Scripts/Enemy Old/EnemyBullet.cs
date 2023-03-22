using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBullet : Bullet
{
    /*void Start()
    {
        rb.velocity = transform.forward * speed * Time.deltaTime;
        startingBulletPosition = rb.transform.position;
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerHealth>().canTakeDamage)
            {
                Debug.Log("Enemy Bullet destroyed by collision!");
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        rb.transform.Translate(0, 0, speed * Time.deltaTime);

        if (rb.transform.position.x > startingBulletPosition.x + maxRange ||
            rb.transform.position.x < startingBulletPosition.x - maxRange)
        {
            Destroy(gameObject);
            Debug.Log("Enemy Bullet destroyed by distance!");
        }
    }
}
