using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyShoot : EnemyShoot
{
    [SerializeField] float timeToShoot = 5;
    [SerializeField] float timeStillAfterShooting = 5;
    public float shootLoad;
    public float stillTimeCounter;
    [SerializeField] Enemy _thisEnemy;
    bool hasShot = false;

    private void Start()
    {
        shootLoad = timeToShoot;
        stillTimeCounter = timeStillAfterShooting;
    }

    void Update()
    {
        if (_thisEnemy.state == Enemy.State.firing)
        {
            if(shootLoad <= 0 && hasShot == false)
            {
                Shoot();
                hasShot = true;
                
            }
            else if (shootLoad <= 0 && hasShot == true)
            {
                if (stillTimeCounter <= 0)
                {
                    shootLoad = timeToShoot;
                    _thisEnemy.state = Enemy.State.moving;
                    stillTimeCounter = timeStillAfterShooting;
                    hasShot = false;
                }
                else
                {
                    stillTimeCounter -= Time.deltaTime;
                }
            }
            else
            {
                shootLoad -= Time.deltaTime;
            }
        }
    }

    enum EnemyState { }
}
