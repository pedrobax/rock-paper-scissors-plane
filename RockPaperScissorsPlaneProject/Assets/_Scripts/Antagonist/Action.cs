using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//parent class for all Actions in the Antagonist system
[System.Serializable]
public class Action : MonoBehaviour
{
    public float duration = 1; //sets how long the action takes to complete
    public Rigidbody rb;
    public bool isActing = false; //is the action currently being performed?
    public bool hasActed = false; //has the action been performed?
    public Transform targetTransform; //target for the action
    public Transform secondaryTargetTransform; //secondary target for the action, not always used
    //public string actionName = "Action"; TODO check and delete this comment

    public virtual void Act() //this is the base Act method which is overridden by child classes
    {
        isActing = true;
        isActing = false;
        hasActed = true; 
    }

    public IEnumerator CountActionDuration(float duration) //counts the duration of the action,
                                                           //used in some child classes but not in most
    {
        yield return new WaitForSeconds(duration);
        isActing = false;
        hasActed = true;
    }
}
