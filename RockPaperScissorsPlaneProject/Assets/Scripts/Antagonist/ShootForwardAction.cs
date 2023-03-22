using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ShootForwardAction : Action
{
    [SerializeField] Transform firePoint;
    BulletHolder bulletHolder;

    private void Start()
    {
        firePoint = transform.GetChild(0);
        bulletHolder = GetComponent<BulletHolder>();
    }

    public override void Act()
    {
        if (!isActing && !hasActed)
        {
            Instantiate(bulletHolder.bullet1Prefab, firePoint.transform.position, firePoint.transform.rotation);
            isActing = true;
            StartCoroutine(CountActionDuration(duration));
        }
    }
}
