using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ShootForwardAction : Action
{
    /* This action shoots a single bullet forward at the start of the duration, then waits for the duration before
     * finishing and moving to the next action in the list
     */
    AntagonistHealth antagonistHealth;
    GameObject muzzleFlashVFX;
    [SerializeField] Transform firePoint;
    BulletHolder bulletHolder; //list of bullets to reference, only uses the first one currently

    private void Start()
    {
        antagonistHealth = GetComponent<AntagonistHealth>();
        muzzleFlashVFX = antagonistHealth.muzzleFlashVFX;
        firePoint = transform.GetChild(0);
        bulletHolder = GetComponent<BulletHolder>();
    }

    public override void Act()
    {
        if (!isActing && !hasActed)
        {
            //shoots the bullet
            Instantiate(bulletHolder.bullet1Prefab, firePoint.transform.position, firePoint.transform.rotation);
            GameObject vfx = Instantiate(muzzleFlashVFX, firePoint.position, firePoint.rotation);
            vfx.transform.parent = this.transform;
            isActing = true;
            StartCoroutine(CountActionDuration(duration));
        }
    }
}
