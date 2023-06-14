using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoBulletAnimation : MonoBehaviour
{
    float rotationSpeed = 70;

    void Start()
    {
        rotationSpeed = Random.Range(70, 120);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }
}
