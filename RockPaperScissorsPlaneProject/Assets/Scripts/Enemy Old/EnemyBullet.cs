using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBullet : Bullet
{
    public AimType aimType = AimType.Straight;
    public float turnSpeed = 5;

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
        if (aimType == AimType.Straight)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else if (aimType == AimType.Player)
        {
            FollowPlayer();
        }
        

        if (transform.position.z > startingBulletPosition.z + maxRange ||
            transform.position.z < startingBulletPosition.z - maxRange)
        {
            Destroy(gameObject);
            Debug.Log("Enemy Bullet destroyed by distance!");
        }
    }

    void FollowPlayer()
    {
        Vector3 relativePosition = GameManager.GetPlayerPosition() - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePosition);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    public enum AimType
    {
        Straight,
        Player,
    }
}
