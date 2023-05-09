using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;

    public void GetRigidBody()
    {
        rb = GetComponent<Rigidbody>();
    }

    //moves rigidbody down on Z axis based on vertical speed
    public void MoveDown(float verticalSpeed)
    {
        Vector3 moveVelocity = new Vector3(0, 0, -verticalSpeed);
        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
    }

    //moves rigidbody to X axis based on horizontal speed
    public void MoveSideways(float horizontalSpeed)
    {
        Vector3 moveVelocity = new Vector3(horizontalSpeed, 0, 0);
        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
    }
}
