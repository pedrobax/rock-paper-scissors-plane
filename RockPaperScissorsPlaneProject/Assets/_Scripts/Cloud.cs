using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Movement
{
    [SerializeField] float teleportDistance = 200; //distance to teleport when hitting limit zone UNUSED
    [SerializeField] float speed; //linear speed of object

    Vector3 teleportDistanceVector = new Vector3(0, 0, 0);

    void Start()
    {
        teleportDistanceVector.z = teleportDistance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * -1 * Time.deltaTime); //moves object based on speed each frame
    }
}
