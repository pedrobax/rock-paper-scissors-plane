using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotateInPlaceAction : Action
{
    public float rotationsQuantity = 1;
    float turnSpeed;
    float fullRotationAngle;

    private void Start()
    {
        fullRotationAngle = rotationsQuantity * 360;
        turnSpeed = fullRotationAngle / duration;
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

    void RotateInPlace()
    {
        transform.rotation *= Quaternion.Euler(0, turnSpeed * Time.deltaTime, 0);
    }
}
