using UnityEngine;
using System.Collections;
using TMPro;

public class ParabolicMovementEaseAction : Action
{
    /* this action is used to make the antagonist move in a parabolic path from the antagonist's current position
     * to the target's position, the path is calculated using the start point, apex point and end point of the path
     * the end point is the target's position, the start point is the antagonist's position, and the apex point is
     * the secondary target's position, the secondary target is used to determine the height of the apex point
     * the movement doesn't pass through the apex point itself, it is used for the calculation of the path
     * 
     * this is the same as ParabolicMovementAction, but with easing in and out applied to the movement
     */

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
            t = EaseInOutQuad(t); // Apply easing function
            transform.position = CalculatePosition(t);
        }
    }

    //parabolic movement function
    private Vector3 CalculatePosition(float t)
    {
        float u = 1.0f - t;
        return (u * u * startPoint) + (2 * u * t * apexPoint) + (t * t * endPoint);
    }

    // Easing function
    private float EaseInOutQuad(float t)
    {
        t = Mathf.Clamp01(t);
        if (t < 0.5f)
        {
            return 2 * t * t;
        }
        else
        {
            return -1 + (4 - 2 * t) * t;
        }
    }
}