using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AntagonistAnimationAction : MonoBehaviour
{
    public int actionIndex;

    public AntagonistAnimation animationSystem;
    public Animator animator;
    public ActionStage stage;

    public string startAnimationName = "empty";
    public string middleAnimationName = "empty";
    public string endAnimationName = "empty";

    public bool startAnimationBool;
    public bool middleAnimationBool;
    public bool endAnimationBool;

    public float startAnimationDuration;
    public float middleAnimationDuration;
    public float endAnimationDuration;

    public float startTime;

    private void Start()
    {
        animationSystem = GetComponent<AntagonistAnimation>();
        animator = animationSystem.animator;

        if (startAnimationDuration + middleAnimationDuration + endAnimationDuration > animationSystem.actionList[actionIndex].duration)
        {
            Debug.Log("ANIMATION" + actionIndex +"'s DURATION IS LONGER THAN ACTION DURATION");
        }
    }

    private void Update()
    {
        if (animationSystem.currentAction != actionIndex)
        {
            stage = ActionStage.None;
        }
        else
        {
            if (animationSystem.currentAction == actionIndex && stage == ActionStage.None)
            {
                Debug.Log("Animation " + actionIndex + " has set start start time");
                startTime = Time.time;
                stage = ActionStage.Start;
            }
            if (stage == ActionStage.Start && Time.time - startAnimationDuration < startTime)
            {
                Debug.Log("Animation " + actionIndex + "'s start animation(" + startAnimationName + ") is running");
                startAnimationBool = true;
            }
            else if (stage == ActionStage.Start && Time.time - startAnimationDuration > startTime)
            {
                Debug.Log("Animation " + actionIndex + " has set middle start time");
                startAnimationBool = false;
                startTime = Time.time;
                stage = ActionStage.Middle;
            }
            if (stage == ActionStage.Middle && Time.time - middleAnimationDuration < startTime)
            {
                Debug.Log("Animation " + actionIndex + "'s middle animation(" + middleAnimationName + ") is running");
                middleAnimationBool = true;
            }
            else if (stage == ActionStage.Middle && Time.time - middleAnimationDuration > startTime)
            {
                Debug.Log("Animation " + actionIndex + " has set end start time");
                middleAnimationBool = false;
                startTime = Time.time;
                stage = ActionStage.End;
            }
            if (stage == ActionStage.End && Time.time - endAnimationDuration < startTime)
            {
                Debug.Log("Animation " + actionIndex + "'s end animation(" + endAnimationName + ") is running");
                endAnimationBool = true;
            }
            else if (stage == ActionStage.End && Time.time - endAnimationDuration > startTime)
            {
                Debug.Log("Animation " + actionIndex + " has finished.");
                endAnimationBool = false;
                stage = ActionStage.Finished;
            }
            SetAnimatorBools();
        }     
    }

    void SetAnimatorBools()
    {
        if (startAnimationName != "empty") animator.SetBool(startAnimationName, startAnimationBool);
        if (middleAnimationName != "empty") animator.SetBool(middleAnimationName, middleAnimationBool);
        if (endAnimationName != "empty") animator.SetBool(endAnimationName, endAnimationBool);
    }

    public enum ActionStage { None, Start, Middle, End, Finished }
}
