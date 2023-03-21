using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : Enemy
{
    [SerializeField] float speedWhileSearching = 3;
    [SerializeField] float maxMovementDuration = 5;
    [SerializeField] float stopBeforeSearchingDuration = 5;
    float stopBeforeSearchingCounter;
    float movementDuration;
    float originalHorizontalSpeed;

    private void Start()
    {
        movementDirection = GetStartingDirection();
        movementDuration = maxMovementDuration;
        stopBeforeSearchingCounter = stopBeforeSearchingDuration;
        originalHorizontalSpeed = horizontalSpeed;
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
            if(_transform.position.y > 0.1 || _transform.position.y < -0.1)
            {
                MoveTowardsLevelY();
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX |
                    RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                horizontalSpeed = originalHorizontalSpeed;
                MoveSidewaysTemp();
                if (movementDuration <= 0)
                {
                    originalHorizontalSpeed = horizontalSpeed;
                    SetStateToSearching();
                    movementDuration = maxMovementDuration;
                }
                TickMovementTime();
            }  
        }
        if (state == State.searching)
        {
            horizontalSpeed = speedWhileSearching;
            if(stopBeforeSearchingCounter <= 0)
            {
                SearchForPlayer();
            }
            else
            {
                stopBeforeSearchingCounter -= Time.deltaTime;
            }
        }
        if (state == State.firing)
        {
            stopBeforeSearchingCounter = stopBeforeSearchingDuration;
            rb.velocity = Vector3.zero;
        }

        void Activate()
        {
            SetStateToMoving();
            BasicEnemyHealth basicEnemyHealth = GetComponent<BasicEnemyHealth>();
            Debug.Log(basicEnemyHealth.unitName + " activated.");
        }

        void TickMovementTime()
        {
            if (movementDuration > 0)
            {
                movementDuration -= 1 * Time.deltaTime;
            }
        }

        void SearchForPlayer()
        {
            Vector3 playerPosition = GameManager.GetPlayerPosition();
            if (_transform.position.x <= playerPosition.x +0.3 && _transform.position.x >= playerPosition.x - 0.3)
            {
                SetStateToFiring();
            }
            else MoveTowardsPlayerXAxis();
        }

        void MoveTowardsPlayerXAxis()
        {
            Vector3 playerPosition = GameManager.GetPlayerPosition();
            if (_transform.position.x < playerPosition.x)
            {
                MoveSideways(+horizontalSpeed);
            }
            if (_transform.position.x  > playerPosition.x)
            {
                MoveSideways(-horizontalSpeed);
            }
        }
    }
}
