using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class AccelerateTowardsPlayerAction : Action
{
    [SerializeField] public float acceleration = 30;
    [SerializeField] float maxSpeed = 150;
    [SerializeField] float turnSpeed = 1;
    Vector3 relativePosition;

    public override void Act()
    {
        //starts the action and its duration
        if (!isActing && !hasActed)
        {
            StartCoroutine(CountMovementDuration(duration));
            isActing = true;
        }
        //turns to face the player and accelerates towards them
        if (isActing)
        {
        TurnTowardsPlayer();
        AccelerateForward();
        }
    }

    //lerps rotation to face Player
    void TurnTowardsPlayer()
    {
        Vector3 relativePosition = GameManager.GetPlayerPosition() - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePosition);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
    }

    //accelerates forward on local Z axis
    void AccelerateForward()
    {
        transform.Translate(0, 0, acceleration * Time.deltaTime);
        if (acceleration > maxSpeed) acceleration += 1;
    }

    IEnumerator CountMovementDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        isActing = false;
        hasActed = true;
    }
}
