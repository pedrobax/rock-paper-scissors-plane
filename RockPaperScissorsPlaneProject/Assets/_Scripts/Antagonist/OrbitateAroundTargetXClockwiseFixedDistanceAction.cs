using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitateAroundTargetXClockwiseFixedDistanceAction : Action
{
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private float fixedDistance = 2f;
    private Vector3 desiredPosition;

    public override void Act()
    {
        if (!isActing && !hasActed)
        {
            StartCoroutine(CountMovementDuration(duration));
            isActing = true;
        }
        if (isActing)
        {
            Vector3 targetPos = targetTransform.position;
            Quaternion targetRot = Quaternion.LookRotation(targetPos - transform.position);

            // Calculate the desired position
            Vector3 offset = targetRot * Vector3.back * fixedDistance;
            desiredPosition = targetPos + offset;

            // Interpolate towards the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 500f);

            // Rotate around the target
            transform.RotateAround(targetPos, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator CountMovementDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        isActing = false;
        hasActed = true;
    }
}