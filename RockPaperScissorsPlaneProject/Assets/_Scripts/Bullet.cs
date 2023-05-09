using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //vfx for when bullet hits something
    public GameObject hitVFX;

    [SerializeField] public float speed = 20f;
    [SerializeField] public float maxRange = 100; //range at which bullet is destroyed after travel
    [SerializeField] public float damage = 1;
    public Vector3 startingBulletPosition;
}
