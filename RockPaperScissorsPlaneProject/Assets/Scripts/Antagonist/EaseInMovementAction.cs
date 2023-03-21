using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class EaseInMovementAction : Action
{
    Vector3 movementVelocity;
    Vector3 targetPosition;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Act()
    {
        targetPosition = targetTransform.position;
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        if (!isActing && !hasActed)
        {
            StartCoroutine(CountMovementDuration(duration));
            isActing = true;
        }
        if (isActing)
        {
            rb.transform.position = transform.position + ((targetPosition - transform.position) * .035f) / duration;
        }
    }

    IEnumerator CountMovementDuration(float duration)
    {
        movementVelocity = targetPosition - transform.position;
        yield return new WaitForSeconds(duration);
        isActing = false;
        hasActed = true;
    }
}
