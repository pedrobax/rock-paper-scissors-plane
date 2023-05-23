using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoBullet : MonoBehaviour
{
    public GameObject pelletPrefab;
    public float explosionTime;
    public float startTime;
    public float bulletAmount;
    public float angleBetweenBullets;
    public Vector3 startPosition;
    public enum BulletType {PELLET, BULLET};
    public BulletType bulletType;
    public float speed;

    void Start()
    {
        startTime = Time.time;
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (startTime + explosionTime <= Time.time && bulletType == BulletType.BULLET)
        {
            Explode();
        }
        if ((transform.position - startPosition).magnitude > 20)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && bulletType == BulletType.BULLET)
        {
            Explode();
        }
        else if (other.CompareTag("Player") && bulletType == BulletType.PELLET)
        {
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        for (int i = 0; i < bulletAmount; i++)
            {
                Instantiate(pelletPrefab, transform.position,
                    transform.rotation * Quaternion.Euler(0f, 0 + (i * angleBetweenBullets), 0));
            }
        Destroy(gameObject);
    }
}
