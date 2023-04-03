using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyHealth : EnemyHealth
{
    private void Update()
    {
        isColliding = false;
    }
}
