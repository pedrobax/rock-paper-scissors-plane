using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinearMovementAction : Action
{
    /* This action is used to make the antagonist move towards a target transform in a straight line
     * the movement speed is based on the duration of the action, so the longer the duration, the slower the movement
     * the movement will always complete at the end of the duration, and the target should always be reached
     */

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

    //moves toward the target transform at a speed based on the duration of the action
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

    //should be called at the start of the movement and sets the movement velocity based on the duration
    //then it waits for the duration before setting isActing to false
    IEnumerator CountMovementDuration(float duration)
    {
        movementVelocity = targetPosition - transform.position;
        yield return new WaitForSeconds(duration);
        isActing = false;
        hasActed = true;
    }
}
