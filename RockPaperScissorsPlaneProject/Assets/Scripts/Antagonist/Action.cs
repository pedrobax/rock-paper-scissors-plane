using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action : MonoBehaviour
{
    public float duration = 1;
    public Rigidbody rb;
    public bool isActing = false;
    public bool hasActed = false;
    public Transform targetTransform;
    public Transform secondaryTargetTransform;
    //public string actionName = "Action";

    public virtual void Act()
    {
        isActing = true;
        isActing = false;
        hasActed = true; 
    }

    public IEnumerator CountActionDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        isActing = false;
        hasActed = true;
    }
}
