using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AntagonistAnimation : MonoBehaviour
{
    public ActionList actionListScript;
    public List<Action> actionList;
    public List<AntagonistAnimationAction> animationActions;
    public Animator animator;
    public int currentAction;

    private void Update()
    {
        currentAction = actionListScript.currentAction;
    }

    public void GetActionList()
    {
        actionList = actionListScript.actionList;
    }

    public void AddAnimation()
    {
        animationActions.Add(this.gameObject.AddComponent<AntagonistAnimationAction>());
        animationActions[animationActions.Count - 1].actionIndex = animationActions.Count - 1;
        animationActions[animationActions.Count - 1].animationSystem = GetComponent<AntagonistAnimation>();
        animationActions[animationActions.Count - 1].animator = animator;
        animationActions[animationActions.Count - 1].startAnimationDuration = actionList[animationActions.Count - 1].duration;
    }
}
