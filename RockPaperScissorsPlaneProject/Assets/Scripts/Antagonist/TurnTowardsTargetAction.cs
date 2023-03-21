using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurnTowardsTargetAction : Action
{
    [SerializeField] float turnSpeed = 5f;
    Vector3 targetPosition;
    //public new string actionName = "TurnTowardsTargetAction";

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Act()
    {
        targetPosition = targetTransform.position;
        TurnTowardsTarget();
    }

    void TurnTowardsTarget()
    {
        if (!isActing && !hasActed)
        {
            StartCoroutine(CountActionDuration(duration));
            isActing = true;
        }
        if (isActing)
        {
            Vector3 relativePosition = targetPosition - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePosition);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
    }
}
