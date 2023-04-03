using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfxSelfDestruct : MonoBehaviour
{
    [SerializeField] int selfDestructDelay = 1;

    void Start()
    {
        Destroy(gameObject, selfDestructDelay);
    }
}
