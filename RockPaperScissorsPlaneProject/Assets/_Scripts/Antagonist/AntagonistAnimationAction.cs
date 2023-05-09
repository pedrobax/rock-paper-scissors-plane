using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AntagonistAnimationAction : MonoBehaviour
{
    /* This class is used in conjunction with the ActionList to animate an enemy based on its current action
     * on the ActionList. Each animation action is an instance of this class, and each is
     * separated into 3 stages: start, middle and end. Each stage has a duration, and an animation, or not,
     * that runs while in the stage duration. The Animation list reads the current action on the action list, and plays
     * the corresponding animation from its list.
     * 
     * The start stage is the first stage of the animation, and it is played when the action starts. It is run until
     * its duration is over, the moves to the middle stage, and then to the end stage.
     * 
     * The complete duration of the start, middle and end stages added together should be equal to the duration of the action. And
     * each stage's duration should be at least 0.1 seconds, even if it doesn't have an animation.
     * 
     * Stages can be left empty, they just won't modify any animator bools.
    */

    public int actionIndex; //should be set to the index of this animation in the animation action list

    public AntagonistAnimation animationSystem;
    public Animator animator;
    public ActionStage stage; //each animation has a none(not being played), start, middle and end stage

    //input name of the animation to be played in each stage
    public string startAnimationName = "empty"; 
    public string middleAnimationName = "empty";
    public string endAnimationName = "empty";

    //works with input name to set the animator bools to true when the animation is being played
    public bool startAnimationBool;
    public bool middleAnimationBool;
    public bool endAnimationBool;

    //sets the duration of each stage of the animation
    //each stage should last at least 0.1 seconds currently
    public float startAnimationDuration;
    public float middleAnimationDuration;
    public float endAnimationDuration;

    //public just for debugging, should be private
    //set the start time for referencing when each stage starts
    public float startTime;

    private void Start()
    {
        animationSystem = GetComponent<AntagonistAnimation>();
        animator = animationSystem.animator;

        //warns if the duration of the animation is longer than the action's duration
        //this is illegal
        if (startAnimationDuration + middleAnimationDuration + endAnimationDuration > animationSystem.actionList[actionIndex].duration)
        {
           // Debug.Log("ANIMATION" + actionIndex +"'s DURATION IS LONGER THAN ACTION DURATION");
        }
    }

    private void Update()
    {
        //guarantees that the animation is not played if it is not the current action
        if (animationSystem.currentAction != actionIndex)
        {
            stage = ActionStage.None;
        }
        else
        {
            //starts the start animation timer
            if (animationSystem.currentAction == actionIndex && stage == ActionStage.None)
            {
                //Debug.Log("Animation " + actionIndex + " has set start start time");
                startTime = Time.time;
                stage = ActionStage.Start;
            }
            //runs the animation if the timer is less than the duration of the animation stage
            if (stage == ActionStage.Start && Time.time - startAnimationDuration < startTime)
            {
                //Debug.Log("Animation " + actionIndex + "'s start animation(" + startAnimationName + ") is running");
                startAnimationBool = true;
            }
            //ends start animation and starts middle animation
            else if (stage == ActionStage.Start && Time.time - startAnimationDuration > startTime)
            {
                //Debug.Log("Animation " + actionIndex + " has set middle start time");
                startAnimationBool = false;
                startTime = Time.time;
                stage = ActionStage.Middle;
            }
            //runs the middle animation if the timer is less than the duration of the animation stage
            if (stage == ActionStage.Middle && Time.time - middleAnimationDuration < startTime)
            {
                //Debug.Log("Animation " + actionIndex + "'s middle animation(" + middleAnimationName + ") is running");
                middleAnimationBool = true;
            }
            //ends middle animation and starts end animation
            else if (stage == ActionStage.Middle && Time.time - middleAnimationDuration > startTime)
            {
                //Debug.Log("Animation " + actionIndex + " has set end start time");
                middleAnimationBool = false;
                startTime = Time.time;
                stage = ActionStage.End;
            }
            //runs end animation if the timer is less than the duration of the animation stage
            if (stage == ActionStage.End && Time.time - endAnimationDuration < startTime)
            {
                //Debug.Log("Animation " + actionIndex + "'s end animation(" + endAnimationName + ") is running");
                endAnimationBool = true;
            }
            //ends end animation and sets the stage to finished, so the next animation can start
            else if (stage == ActionStage.End && Time.time - endAnimationDuration > startTime)
            {
                //Debug.Log("Animation " + actionIndex + " has finished.");
                endAnimationBool = false;
                stage = ActionStage.Finished;
            }
            //sets the correct animation bools on the animator based on the current stage being performed
            SetAnimatorBools();
        }     
    }

    //sets the correct animation bools on the animator based on the current stage being performed
    void SetAnimatorBools()
    {
        if (startAnimationName != "empty") animator.SetBool(startAnimationName, startAnimationBool);
        if (middleAnimationName != "empty") animator.SetBool(middleAnimationName, middleAnimationBool);
        if (endAnimationName != "empty") animator.SetBool(endAnimationName, endAnimationBool);
    }

    //used to reference the current stage
    public enum ActionStage { None, Start, Middle, End, Finished }
}
