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
        teleportDistanceVector.z = teleportDistance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * -1 * Time.deltaTime);
    }
}
