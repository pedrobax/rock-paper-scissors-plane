using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    void Update()
    {
        if (transform.rotation.z > 0 && Input.GetAxis("Horizontal") > 0)
        {
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (transform.rotation.z < 0 && Input.GetAxis("Horizontal") < 0)
        {
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            Quaternion newRotation = Quaternion.Euler(0f, 0f, 45f * Input.GetAxis("Horizontal") * -1);
            this.transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime);
        }
    }
}
