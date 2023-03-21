using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackToActionXAction : Action
{
    ActionList _actionList;
    [SerializeField] public int action;

    public override void Act()
    {
        _actionList = GetComponent<ActionList>();
        if (!isActing && !hasActed)
        {
            isActing = true;
            StartCoroutine(GoBackToActionX());
        }
    }
    
    IEnumerator GoBackToActionX()
    {
        yield return new WaitForSeconds(duration);
        _actionList.currentAction = action;
        isActing = false;
        hasActed = true;
        for (int i = 0; i < _actionList.actionList.Count; i++)
        {
            _actionList.actionList[i].hasActed = false;
        }
    }
}
