using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinearMovementAction : Action
{
    Vector3 movementVelocity;
    Vector3 targetPosition;

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
            StartCoroutine(CountMovementDuration(duration));
            isActing = true;
        }
        if (isActing) 
        {
            rb.MovePosition(rb.position + movementVelocity / duration * Time.deltaTime);
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
