using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructAction : Action
{
    //public new string actionName = "SelfDestructAction";
    public override void Act()
    {
        Destroy(gameObject.transform.parent.gameObject, duration);
    }
}
