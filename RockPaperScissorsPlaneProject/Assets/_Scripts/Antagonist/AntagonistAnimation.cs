using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AntagonistAnimation : MonoBehaviour
{
    public ActionList actionListScript; //references object's action list
    public List<Action> actionList; //stores the referenced action list
    public List<AntagonistAnimationAction> animationActions; //list of animations to be played, each animation
                                                             //is an instance of the animation action class
    public Animator animator;
    public int currentAction; //current animation being played

    private void Start()
    {
        //all of these are just backups in case something has not been set correctly in editor
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (actionListScript == null)
        {
            actionListScript = GetComponent<ActionList>();
            if (actionListScript == null)
            {
                GetComponentInParent<ActionList>();
            }
                GetActionList();
        }
    }

    private void Update()
    {
        currentAction = actionListScript.currentAction; //makes the current animation action the same
                                                        //as the action list's current action
    }

    public void GetActionList()
    {
        actionList = actionListScript.actionList; //copies the action list from the action list script
                                                  //is used as a button on inspector
    }

    public void AddAnimation() //adds an animation action to the list, is a button on inspector
    {
        animationActions.Add(this.gameObject.AddComponent<AntagonistAnimationAction>());
        animationActions[animationActions.Count - 1].actionIndex = animationActions.Count - 1;
        animationActions[animationActions.Count - 1].animationSystem = GetComponent<AntagonistAnimation>();
        animationActions[animationActions.Count - 1].animator = animator;
        animationActions[animationActions.Count - 1].startAnimationDuration = actionList[animationActions.Count - 1].duration;
    }
}
