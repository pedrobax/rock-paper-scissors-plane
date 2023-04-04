using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMovement : Movement
{
    [SerializeField] float speed;
    void Start()
    {
        GetRigidBody();
    }

    void Update()
    {
        if(transform.position.z > 5) MoveDown(speed);
    }
}
