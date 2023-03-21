using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ShootForwardAction : Action
{
    [SerializeField] Transform firePoint;
    BulletHolder bulletHolder;
    //public new string actionName = "ShootForwardAction";

    public override void Act()
    {
        if (!isActing && !hasActed)
        {
            bulletHolder = GetComponent<BulletHolder>();
            Instantiate(bulletHolder.bullet1Prefab, transform.position, transform.rotation);
            isActing = true;
            StartCoroutine(CountActionDuration(duration));
        }
    }
}
