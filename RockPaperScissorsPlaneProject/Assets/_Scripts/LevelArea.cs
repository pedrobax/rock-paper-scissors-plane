using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArea : MonoBehaviour
{
    public float horizontalAreaLimit;
    public float verticalAreaLimit;

    void Awake()
    {
        SetLevelArea();
    }

    void SetLevelArea()
    {
        horizontalAreaLimit = transform.localScale.x / 2;
        verticalAreaLimit = transform.localScale.z / 2;
    }

    public float GetVerticalAreaLimit()
    {
        return verticalAreaLimit;
    }
    public float GetHorizontalAreaLimit()
    {
        return horizontalAreaLimit;
    }
}
