using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemBondFire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject firePoint;
    public GameObject fireVfx;

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        GameObject vfx = Instantiate(fireVfx, firePoint.transform.position, firePoint.transform.rotation);
        vfx.transform.localScale = Vector3.one * 3;
        vfx.transform.position += vfx.transform.forward * 5f;
    }
}
