using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMovement : Movement
{
    //TODO merge all of this class into Pickup.cs

    [SerializeField] float speed;
    [SerializeField] float maxPosition;
    void Start()
    {
        GetRigidBody();
    }

    void Update()
    {
        if(transform.position.z > maxPosition) MoveDown(speed); //moves the pickup down if it isn't at max position
    }
}
