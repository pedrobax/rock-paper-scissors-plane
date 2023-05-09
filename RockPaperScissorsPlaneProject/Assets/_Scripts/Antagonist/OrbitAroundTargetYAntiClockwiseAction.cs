using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAroundTargetYAntiClockwiseAction : Action
{
    /* this action is used to make the antagonist orbit around a target transform in an anti clockwise manner,
     * the circle's radius is set by the distance between the antagonist and the target at the start of the action,
     * and will always remain the same, the speed determines how fast the antagonist will orbit around the target
     */

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
            //this rotates the antagonist around the target
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
