using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBullet : Bullet
{
    private void Start()
    {
        startingBulletPosition = transform.position;
    }

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
        transform.Translate(0, 0, speed * Time.deltaTime);

        if (transform.position.x > startingBulletPosition.x + maxRange ||
            transform.position.x < startingBulletPosition.x - maxRange)
        {
            Destroy(gameObject);
            Debug.Log("Enemy Bullet destroyed by distance!");
        }
    }
}
