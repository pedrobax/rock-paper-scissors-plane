using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Movement
{
    [SerializeField] float teleportDistance = 200;
    [SerializeField] float speed;

    Vector3 teleportDistanceVector = new Vector3(0, 0, 0);

    void Start()
    {
        GetRigidBody();
        teleportDistanceVector.z = teleportDistance;
    }

    // Update is called once per frame
    void Update()
    {
        MoveDown(speed);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cloud TP Zone")
        {
            rb.position = rb.GetComponent<Rigidbody>().position + teleportDistanceVector; 
        }
    }*/
}
