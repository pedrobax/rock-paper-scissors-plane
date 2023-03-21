using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAroundTargetYAntiClockwiseAction : Action
{
    [SerializeField] public float rotationSpeed = 20;

    public override void Act()
    {
        if (!isActing && !hasActed)
        {
            StartCoroutine(CountMovementDuration(duration));
            isActing = true;
        }
        if (isActing)
        {
            transform.RotateAround(targetTransform.position, Vector3.down, rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator CountMovementDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        isActing = false;
        hasActed = true;
    }
}
