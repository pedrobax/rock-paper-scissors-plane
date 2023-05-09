using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinearMovementEaseAction : Action
{
    /* this action is used to make the antagonist move towards a target transform in a straight line, but with an
     * ease in and ease out animation speed
     * the movement speed is based on the duration of the action, so the longer the duration, the slower the movement
     * the movement will always complete at the end of the duration, and the target should always be reached
     */

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float startTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Act()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = targetTransform.position;
        MoveTowardsTarget();
    }

    //moves toward the target transform at a speed based on the duration and easing functions
    void MoveTowardsTarget()
    {
        if (!isActing && !hasActed)
        {
            startPosition = transform.position;
            startTime = Time.time;
            StartCoroutine(CountMovementDuration(duration));
            isActing = true;
        }
        if (isActing)
        {
            float t = (Time.fixedTime - startTime) / duration;
            t = EaseInOutQuad(t);
            rb.MovePosition(Vector3.Lerp(startPosition, targetPosition, t));
        }
    }

    //waits for time to be over and ends in the next fixed update
    IEnumerator CountMovementDuration(float duration)
    {
        while (Time.time - startTime < duration)
        {
            yield return new WaitForFixedUpdate();
        }
        isActing = false;
        hasActed = true;
    }

    //easing function
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