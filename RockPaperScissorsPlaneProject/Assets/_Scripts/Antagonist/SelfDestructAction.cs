using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructAction : Action
{
    //this action just destroy the object after the set duration, is usually used as the last action for 
    //antagonists that need to self destruct for any reason

    //public new string actionName = "SelfDestructAction";
    public override void Act()
    {
        Destroy(gameObject.transform.parent.gameObject, duration);
    }
}
