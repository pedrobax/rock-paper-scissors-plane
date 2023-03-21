using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularScissorsMovement : Enemy
{
    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed;
    public float turnSpeed = 1;
    [SerializeField] float searchingTurnSpeed = 5;
    [SerializeField] float firingTurnSpeed = 3;
    [SerializeField] Transform playerTransform;
    [SerializeField] float stopBeforeSearchingDuration = 5;
    float stopBeforeSearchingCounter;
    Vector3 playerPosition;
    Vector3 relativePosition;

    private void Start()
    {
        state = State.inactive;
        stopBeforeSearchingCounter = stopBeforeSearchingDuration;
    }

    private void Update()
    {
        if (state == State.inactive)
        {
            MoveDown(verticalSpeed);
            if (rb.position.z < GameManager.GetActivationArea() + activatePositionModifier)
            {
                Activate();
                Debug.Log(activatePositionModifier);
            }
        }
        if (state == State.moving)
        {
            if (_transform.position.y > 0.1 || _transform.position.y < -0.1)
            {
                MoveTowardsLevelY();
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX
                    | RigidbodyConstraints.FreezeRotationZ;
                SetStateToSearching();
            }

        }
        if (state == State.searching)
        {
            turnSpeed = searchingTurnSpeed;
            TurnTowardsPlayer();
            if (stopBeforeSearchingCounter <= 0)
            {
                SetStateToFiring();
            }
            else
            {
                stopBeforeSearchingCounter -= Time.deltaTime;
            }
        }
        if (state == State.firing)
        {
            turnSpeed = firingTurnSpeed;

            if (transform.position.z > playerPosition.z) TurnTowardsPlayer();
            AccelerateForward();
        }
    }

    void TurnTowardsPlayer()
    {
        playerPosition = playerTransform.position;
        relativePosition = _transform.position - playerPosition;
        Quaternion toRotation = Quaternion.LookRotation(relativePosition);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
    }
 
    void AccelerateForward()
    {
        transform.Translate(0, 0, -acceleration * Time.deltaTime);
        if (acceleration > maxSpeed) acceleration += 1;
    }

    void Activate()
    {
        SetStateToMoving();
        RegularScissorsHealth regularScissorsHealth = GetComponent<RegularScissorsHealth>();
        Debug.Log(regularScissorsHealth.unitName + " activated.");
    }
}
