using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBarrageForwardAction : Action
{
    /* This action is used to make the antagonist shoot a barrage of bullets forward, all bullets are shot at once
     * the number of bullets is set and shot divided by the set angle, this is usually done to achieve a shotgun effect,
     * but it can also be used up to 360, to make the antagonist shoot in all directions
     * 
     * an interesting note is that when using an even amount of bullets, no bullets will be shot directly forward,
     * but with an odd number always there will be. This can be used in conjunction with an action to aim at the player
     * to create situations where he is at shot directly and must change his position accordingly
     */

    [SerializeField] Transform firePoint;
    [SerializeField] public float bulletAmount = 3; //how many bullets will be shot
    private float angleBetweenBullets; //internely calculates the angle between each bullet
    BulletHolder bulletHolder;
    public float shootingAngle; //the complete angle the bullets will be shot in

    private void Start()
    {
        firePoint = transform.GetChild(0);
        bulletHolder = GetComponent<BulletHolder>();
        angleBetweenBullets = shootingAngle / bulletAmount; //calculates the angle between bullets based on
                                                            //the bullet amount divided by the set angle
    }

    public override void Act()
    {
        if (!isActing && !hasActed)
        {
            //spawns each bullet at the same time. The direction of each is based on the current loop and the angle between bullets
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
