using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class OrbitAroundTargetYClockwiseAction : Action
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
            transform.RotateAround(targetTransform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
    IEnumerator CountMovementDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        isActing = false;
        hasActed = true;
    }
}
