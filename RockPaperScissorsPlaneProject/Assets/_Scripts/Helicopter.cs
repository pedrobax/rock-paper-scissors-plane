using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField] float speed;
    private void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
