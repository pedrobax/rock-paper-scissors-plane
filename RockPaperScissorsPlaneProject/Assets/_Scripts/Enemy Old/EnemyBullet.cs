using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class EnemyBullet : Bullet
{
    public AimType aimType = AimType.Straight;
    public float turnSpeed = 5;
    bool isTweening = false;
    public float arcDuration;
    public float arcSize;
    public float selfDestructTime = 0;

    private void Start()
    {
        startingBulletPosition = transform.position;
        if (selfDestructTime > 0) Destroy(gameObject, selfDestructTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerHealth>().canTakeDamage)
            {
                //Debug.Log("Enemy Bullet destroyed by collision!");
                Instantiate(hitVFX, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
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
        else if (aimType == AimType.Arc && isTweening == false)
        {
            transform.DOScaleX(arcSize, arcDuration);
            transform.DOScaleZ(arcSize, arcDuration);
            isTweening = true;
        }
        

        if (transform.position.z > startingBulletPosition.z + maxRange ||
            transform.position.z < startingBulletPosition.z - maxRange)
        {
            Destroy(gameObject);
            //Debug.Log("Enemy Bullet destroyed by distance!");
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
        Arc,
    }
}
