using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireCooldown;
    float fireCooldownTimer;
    bool canShoot = true;
    public GameObject basicBulletPrefab;
    public Transform firePoint;

    private void Start()
    {
        fireCooldownTimer = 0;
    }

    public void Shoot()
    {
        Instantiate(basicBulletPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Shots fired!");
    }

    public void ShootOnCooldown()
    {
        if (canShoot)
        {
            Shoot();
            canShoot = false;
            fireCooldownTimer = fireCooldown;
        }
    }

    public void TickCooldownTimer()
    {
        if (fireCooldownTimer <= 0) canShoot = true;
        if (fireCooldownTimer > 0)
        {
            fireCooldownTimer -= 1 * Time.deltaTime;
        }
    }
}
