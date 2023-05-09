using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;

    void Update()
    {
        transform.Rotate(_rotation * Time.deltaTime); //rotates the object on the x, y, and z axis based on the rotation vector
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") Destroy(gameObject); //destroys the pickup when the player collects it
    }
}
