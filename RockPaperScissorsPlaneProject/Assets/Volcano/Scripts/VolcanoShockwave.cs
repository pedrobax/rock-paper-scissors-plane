using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoShockwave : MonoBehaviour
{
    public enum BulletType { Paper, Rock, Scissors };
    public BulletType bulletType;
    public GameObject firePoint;
    public GameObject paperBullet, rockBullet, scissorsBullet;
    public float bulletAmount = 3; //how many bullets will be shot
    private float angleBetweenBullets; //internely calculates the angle between each bullet
    public float shootingAngle; //the complete angle the bullets will be shot in
    public float fireCooldown;
    float currentCooldown;
    public float startTime;
    public Volcano volcano;

    void Start()
    {
        angleBetweenBullets = shootingAngle / bulletAmount; //calculates the angle between bullets based on
                                                            //the bullet amount divided by the set angle
        currentCooldown = Time.time + startTime;
    }

    void FixedUpdate()
    {
        if(!volcano.isDefeated)
        {
            if (Time.time - currentCooldown >= fireCooldown)
            {
                bulletType = (BulletType)Random.Range(0, 3);
                Debug.Log(bulletType);
                Shoot();
                currentCooldown = Time.time;
            }
        }
    }

    void Shoot()
    {
        if (bulletType == BulletType.Paper)
        {
            for (int i = 0; i < bulletAmount; i++)
                 {
                     Instantiate(paperBullet, firePoint.transform.position,
                         firePoint.transform.rotation * Quaternion.Euler(0f, (angleBetweenBullets * (i - bulletAmount/2)) + angleBetweenBullets/2, 0f));
                 }
        }
        else if (bulletType == BulletType.Rock)
        {
            for (int i = 0; i < bulletAmount; i++)
                 {
                     Instantiate(rockBullet, firePoint.transform.position,
                         firePoint.transform.rotation * Quaternion.Euler(0f, (angleBetweenBullets * (i - bulletAmount/2)) + angleBetweenBullets/2, 0f));
                 }
        }
        else if (bulletType == BulletType.Scissors)
        {
            for (int i = 0; i < bulletAmount; i++)
                 {
                     Instantiate(scissorsBullet, firePoint.transform.position,
                         firePoint.transform.rotation * Quaternion.Euler(0f, (angleBetweenBullets * (i - bulletAmount/2)) + angleBetweenBullets/2, 0f));
                 }
        }
    }
}
