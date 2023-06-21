using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;
    public float speed = 6f;

    void Update()
    {
        transform.Rotate(_rotation * Time.deltaTime); //rotates the object on the x, y, and z axis based on the rotation vector
        transform.position += Vector3.back * speed * Time.deltaTime; //moves the object down based on the speed variable
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") Destroy(gameObject); //destroys the pickup when the player collects it
    }
}
