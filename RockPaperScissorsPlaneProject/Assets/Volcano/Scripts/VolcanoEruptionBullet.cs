using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoEruptionBullet : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 direction;
    public float eruptionForce = 5;
    Rigidbody rb;
    bool isOnLevelArea = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction * eruptionForce, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        if (rb.position.y < 0)
        {
            rb.MovePosition(new Vector3(rb.position.x, 0, rb.position.z));
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            isOnLevelArea = true;
        }
        if (isOnLevelArea)
        {
            rb.MovePosition(new Vector3(rb.position.x, rb.position.y, rb.position.z - speed * Time.fixedDeltaTime));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
