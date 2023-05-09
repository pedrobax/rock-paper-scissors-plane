using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackToActionXAction : Action
{
    /*this action is used to make the antagonist go back to a specific previous action in the action list,
     *effectively creating an action loop that doesn't go all the way to the start
     *this will always be the last action in the action list, since there are no Actions to skip to other actions
     *it waits for the set duration before going back to the selected action index
     *it also has a reference to the action list, so it can function when working with an antagonist with multiple action lists
     */

    public ActionList _actionList;
    [SerializeField] public int action;

    public override void Act()
    {
        if (!isActing && !hasActed)
        {
            isActing = true;
            StartCoroutine(GoBackToActionX());
        }
    }
    
    //works with the actionList script directly to change the current action to the new set one
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
