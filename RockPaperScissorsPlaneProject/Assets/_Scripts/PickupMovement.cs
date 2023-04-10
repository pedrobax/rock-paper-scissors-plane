using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMovement : Movement
{
    [SerializeField] float speed;
    [SerializeField] float maxPosition;
    void Start()
    {
        GetRigidBody();
    }

    void Update()
    {
        if(transform.position.z > maxPosition) MoveDown(speed);
    }
}
