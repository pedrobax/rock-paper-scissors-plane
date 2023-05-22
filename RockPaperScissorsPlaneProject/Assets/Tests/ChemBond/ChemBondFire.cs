using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemBondFire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject firePoint;
    public GameObject fireVfx;

    [SerializeField] public float bulletAmount = 3; //how many bullets will be shot
    private float angleBetweenBullets; //internely calculates the angle between each bullet
    public float shootingAngle; //the complete angle the bullets will be shot in

    void Start()
    {
        angleBetweenBullets = shootingAngle / bulletAmount; //calculates the angle between bullets based on
                                                            //the bullet amount divided by the set angle
    }

    //shoots amount of bullets divided in the angle
    void Shoot()
    {
        for (int i = 0; i < bulletAmount; i++)
            {
                Instantiate(bulletPrefab, firePoint.transform.position,
                    firePoint.transform.rotation * Quaternion.Euler(0f, (angleBetweenBullets * (i - bulletAmount/2)) + angleBetweenBullets/2, 0f));
            }
        GameObject vfx = Instantiate(fireVfx, firePoint.transform.position, firePoint.transform.rotation);
        vfx.transform.localScale = Vector3.one * 3;
        vfx.transform.position += vfx.transform.forward * 5f;
    }
}
