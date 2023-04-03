using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : Movement
{
    [SerializeField] public float verticalSpeed;
    [SerializeField] public float horizontalSpeed;
    [SerializeField] public Side startingDirection;
    public Side movementDirection;
    public State state = State.inactive;

    public void MoveSidewaysTemp()
    {
        if (rb.transform.position.z >= 12) SetMovementDirection(Side.right);
        if (rb.transform.position.z <= -12) SetMovementDirection(Side.left);
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

    public void SetStateActive()
    {
        state = State.active;
    }

    public enum State { inactive, active}
    public enum Side { left, right }
}
