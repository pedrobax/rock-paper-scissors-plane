using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefabBasic;
    public GameObject bulletPrefabSuper;
    public float cooldown = 1;
    float cooldownTimer;
    bool isSuperUpgraded = false;
    public PlayerHealth playerHealth;

    void Update()
    {

        TickCooldown();

        if (Input.GetButton("Fire1") && cooldownTimer == 0 && playerHealth.canTakeDamage == true)
        {
            Shoot();
            cooldownTimer = cooldown;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet Power Up")
        {
            isSuperUpgraded = true;
        }
    }

    void Shoot()
    {
        if (isSuperUpgraded) ShootSuperBullet();
        else ShootBasicBullet();
    }

    void ShootBasicBullet()
    {
        Instantiate(bulletPrefabBasic, firePoint.position, firePoint.rotation);
    }
    void ShootSuperBullet()
    {
        Instantiate(bulletPrefabSuper, firePoint.position, firePoint.rotation);
    }
    void TickCooldown()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        if (cooldownTimer < 0) cooldownTimer = 0;
    }
}
