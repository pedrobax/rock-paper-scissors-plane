using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfxSelfDestruct : MonoBehaviour
{
    [SerializeField] int selfDestructDelay = 1;

    //destroys the vfx instance a set time after it instantiates
    void Start()
    {
        Destroy(gameObject, selfDestructDelay);
    }
}
