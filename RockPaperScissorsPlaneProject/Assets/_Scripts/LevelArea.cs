using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArea : MonoBehaviour
{
    //limits to define player's movable area
    public float horizontalAreaLimit;
    public float verticalAreaLimit;

    void Awake()
    {
        SetLevelArea();
    }

    //set the limits of the level area based on the xz scale of the object
    void SetLevelArea()
    {
        horizontalAreaLimit = transform.localScale.x / 2;
        verticalAreaLimit = transform.localScale.z / 2;
    }

    //returns the limits of the level area for referencing
    public float GetVerticalAreaLimit()
    {
        return verticalAreaLimit;
    }
    public float GetHorizontalAreaLimit()
    {
        return horizontalAreaLimit;
    }
}
