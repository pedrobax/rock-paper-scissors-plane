using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotateInPlaceAction : Action
{
    /* this action is used to make the antagonist rotate in place, but can also be used as a "wait in place"
     * with the rotations quantity set to 0. The action will rotate times the set quantity in the duration
     * 
     * the rotation speed is calculated in the Start method, so it can't be changed during runtime
     */

    public float rotationsQuantity = 1; //sets how many times the antagonist will rotate in the duration
    float turnSpeed; //internely calculated turn speed
    float fullRotationAngle; //full rotation angle based on the rotations quantity

    private void Start()
    {
        fullRotationAngle = rotationsQuantity * 360; //calculates the full rotation angle
        turnSpeed = fullRotationAngle / duration; //calculates the turn speed
    }

    public override void Act()
    {
        if (!isActing && !hasActed)
        {
            StartCoroutine(CountActionDuration(duration));
            isActing = true;
        }
        if (isActing)
        {
            RotateInPlace();
        }
    }

    //rotates the antagonist in place based on the turn speed
    void RotateInPlace()
    {
        transform.rotation *= Quaternion.Euler(0, turnSpeed * Time.deltaTime, 0);
    }
}
