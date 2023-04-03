using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnTowardsPlayerAction : Action
{
    [SerializeField] float turnSpeed = 5f;
    Vector3 targetPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Act()
    {
        targetPosition = GameManager.GetPlayerPosition();
        TurnTowardsPlayer();
    }

    void TurnTowardsPlayer()
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
