using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed = 20f;
    [SerializeField] public float maxRange = 100;
    [SerializeField] public float damage = 1;
    public Rigidbody rb;
    public Vector3 startingBulletPosition;
}
