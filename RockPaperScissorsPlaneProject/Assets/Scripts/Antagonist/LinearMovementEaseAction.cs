using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinearMovementEaseAction : Action
{
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
            float t = (Time.time - startTime) / duration;
            t = EaseInOutQuad(t);
            rb.MovePosition(Vector3.Lerp(startPosition, targetPosition, t));
        }
    }

    IEnumerator CountMovementDuration(float duration)
    {
        while (Time.time - startTime < duration)
        {
            yield return null;
        }
        isActing = false;
        hasActed = true;
    }

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