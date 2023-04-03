using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBarrageForwardAction : Action
{
    [SerializeField] Transform firePoint;
    [SerializeField] public float bulletAmount = 3;
    private float angleBetweenBullets;
    BulletHolder bulletHolder;
    public float shootingAngle;

    private void Start()
    {
        firePoint = transform.GetChild(0);
        bulletHolder = GetComponent<BulletHolder>();
        angleBetweenBullets = shootingAngle / bulletAmount;
    }

    public override void Act()
    {
        if (!isActing && !hasActed)
        {
            for (int i = 0; i < bulletAmount; i++)
            {
                Instantiate(bulletHolder.bullet1Prefab, firePoint.transform.position,
                    firePoint.transform.rotation * Quaternion.Euler(0f, (angleBetweenBullets * (i - bulletAmount/2)) + angleBetweenBullets/2, 0f));
            }
            isActing = true;
            StartCoroutine(CountActionDuration(duration));
        }
    }
}
