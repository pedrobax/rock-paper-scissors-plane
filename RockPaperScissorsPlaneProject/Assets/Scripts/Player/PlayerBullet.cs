using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    /*void Start()
    {
        rb.velocity = transform.right * speed * Time.deltaTime;
        startingBulletPosition = rb.transform.position;
    }*/


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyRock") ||
            other.CompareTag("EnemyPaper") || other.CompareTag("EnemyScissors"))
        {
            Debug.Log("Player Bullet destroyed by collision!");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        rb.transform.Translate(0, 0, speed * Time.deltaTime);

        if (rb.transform.position.x > startingBulletPosition.x + maxRange ||
            rb.transform.position.x < startingBulletPosition.x - maxRange)
        {
            Destroy(gameObject);
            Debug.Log("Player Bullet destroyed by distance!");
        }
    }
}
