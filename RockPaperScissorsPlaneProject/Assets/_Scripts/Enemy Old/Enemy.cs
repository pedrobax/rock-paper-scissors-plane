using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Movement
{
    [SerializeField] public float verticalSpeed;
    [SerializeField] public float horizontalSpeed;
    [SerializeField] public float spawnSpeed;
    [SerializeField] public Side startingDirection;
    [SerializeField] public float activatePositionModifier = 5;
    public Transform _transform;
    public Side movementDirection;
    public State state = State.inactive;

    public void MoveSidewaysTemp()
    {
        if (rb.transform.position.x >= 12) SetMovementDirection(Side.right);
        if (rb.transform.position.x <= -12) SetMovementDirection(Side.left);
        if (movementDirection == Side.left) MoveSideways(+horizontalSpeed);
        if (movementDirection == Side.right) MoveSideways(-horizontalSpeed);
    }

    public void SetMovementDirection(Side direction)
    {
        movementDirection = direction;
    }

    public Side GetStartingDirection()
    {
        return startingDirection;
    }

    public void SetStateToInactive()
    {
        state = State.inactive;
    }

    public void SetStateToMoving()
    {
        state = State.moving;
    }
    public void SetStateToSearching()
    {
        state = State.searching;
    }
    public void SetStateToFiring()
    {
        state = State.firing;
    }
    public void MoveTowardsLevelY()
    {
        if (_transform.position.y > 0.1)
        {
            Vector3 velocity = new Vector3(0, spawnSpeed, 0);
            rb.MovePosition(rb.position - velocity * Time.deltaTime);
            // Debug.Log("Moving down");
        }
        if (_transform.position.y < -0.1)
        {
            Vector3 velocity = new Vector3(0, spawnSpeed, 0);
            rb.MovePosition(rb.position + velocity * Time.deltaTime);
            // Debug.Log("Moving up");
        }

    }

    public enum State { inactive, searching, moving, firing}
    public enum Side { left, right }
}
