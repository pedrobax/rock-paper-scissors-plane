using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;

    void Update()
    {
        transform.Rotate(_rotation * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") Destroy(gameObject);
    }
}
