using UnityEngine;
using System.Collections;
using TMPro;

public class ParabolicMovementAction : Action
{
    private Vector3 startPoint;
    private Vector3 apexPoint;
    private Vector3 endPoint;

    private float startTime;

    public override void Act()
    {
        if (!isActing && !hasActed)
        {
            apexPoint = secondaryTargetTransform.position;
            startPoint = transform.position;
            endPoint = targetTransform.position;
            startTime = Time.time;
            isActing = true;
            StartCoroutine(CountActionDuration(duration));
        }
        if (isActing)
        {
            float t = (Time.time - startTime) / duration;
            transform.position = CalculatePosition(t);
        }
    }

    private Vector3 CalculatePosition(float t)
    {
        float u = 1.0f - t;
        return (u * u * startPoint) + (2 * u * t * apexPoint) + (t * t * endPoint);
    }
}