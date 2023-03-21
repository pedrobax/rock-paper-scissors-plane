using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antagonist : MonoBehaviour
{
    Rigidbody rb;

    public float currentMovementStep = 0;
    float actionDuration = 5;

    Vector3 targetPosition;
    Vector3 movementVelocity;

    [SerializeField] float turnSpeed = 1;
    public GameObject bulletPrefab;
    public Transform firePoint;

    //set the type for each action in order, very unintuitive
    [SerializeField] ActionType firstActionType;
    [SerializeField] ActionType secondActionType;
    [SerializeField] ActionType thirdActionType;
    [SerializeField] ActionType fourthActionType;
    [SerializeField] ActionType fifthActionType;
    [SerializeField] ActionType sixthActionType;
    [SerializeField] ActionType seventhActionType;
    [SerializeField] ActionType eighthActionType;

    //set the duration for each action in order, very unintuitive
    [SerializeField] float firstStepMovementDuration = 1;
    Vector3 firstStepTargetPosition = Vector3.zero;

    [SerializeField] float secondStepMovementDuration = 1;
    Vector3 secondStepTargetPosition = Vector3.zero;

    [SerializeField] float thirdStepMovementDuration = 1;
    Vector3 thirdStepTargetPosition = Vector3.zero;

    [SerializeField] float fourthStepMovementDuration = 1;
    Vector3 fourthStepTargetPosition = Vector3.zero;

    [SerializeField] float fifthStepMovementDuration = 1;
    Vector3 fifthStepTargetPosition = Vector3.zero;

    [SerializeField] float sixthStepMovementDuration = 1;
    Vector3 sixthStepTargetPosition = Vector3.zero;

    [SerializeField] float seventhStepMovementDuration = 1;
    Vector3 seventhStepTargetPosition = Vector3.zero;

    [SerializeField] float eighthStepMovementDuration = 1;
    Vector3 eighthStepTargetPosition = Vector3.zero;

    //set the Transform instance target for each action in order, very unintuitive
    [SerializeField] Transform step1TargetTransform;
    [SerializeField] Transform step2TargetTransform;
    [SerializeField] Transform step3TargetTransform;
    [SerializeField] Transform step4TargetTransform;
    [SerializeField] Transform step5TargetTransform;
    [SerializeField] Transform step6TargetTransform;
    [SerializeField] Transform step7TargetTransform;
    [SerializeField] Transform step8TargetTransform;

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, step1TargetTransform.position);
        Gizmos.DrawLine(transform.position, step3TargetTransform.position);
        Gizmos.DrawLine(transform.position, step5TargetTransform.position);
        Gizmos.DrawLine(transform.position, step7TargetTransform.position);
        /* Gizmos.DrawLine(step1TargetTransform.position, step2TargetTransform.position);
        Gizmos.DrawLine(step2TargetTransform.position, step3TargetTransform.position);
        Gizmos.DrawLine(step3TargetTransform.position, step4TargetTransform.position);
        Gizmos.DrawLine(step4TargetTransform.position, step5TargetTransform.position);
        Gizmos.DrawLine(step5TargetTransform.position, step6TargetTransform.position);
        Gizmos.DrawLine(step6TargetTransform.position, step7TargetTransform.position);
        Gizmos.DrawLine(step7TargetTransform.position, step8TargetTransform.position); */
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Runs actions on a fixed number, change so there is only one method that changes numbers and with it the actions
        RunAction(1);
        RunAction(2);
        RunAction(3);
        RunAction(4);
        RunAction(5);
        RunAction(6);
        RunAction(7);
        RunAction(8);
        StartCoroutine(WaitAndReturnCurrentStepTo1(9, 0.1f));
    }

    /*This runs the action based on the action number and the action type. This is very unintuitive and needs to be changed.
    I'll create a class for action and then create a class instance for each type of action.
    Then I'll create a list of action instances and run them in order. So then I can add and subtract any number of actions easily */
    void RunAction(float actionNumber)
    {
        switch (actionNumber)
        {
            case 1:
                switch (firstActionType)
                {
                    case 0: MoveTowardsTarget(actionNumber); break;
                    case (ActionType)1: TurnTowardsTarget(actionNumber); break;
                    case (ActionType)2: FireForward(actionNumber); break;
                    case (ActionType)3: StartCoroutine(WaitAndReturnCurrentStepTo1(actionNumber, 0.1f)); break;
                }
                break;

            case 2:
                switch (secondActionType)
                {
                    case 0: MoveTowardsTarget(actionNumber); break;
                    case (ActionType)1: TurnTowardsTarget(actionNumber); break;
                    case (ActionType)2: FireForward(actionNumber); break;
                    case (ActionType)3: StartCoroutine(WaitAndReturnCurrentStepTo1(actionNumber, 0.1f)); break;
                }
                break;

            case 3:
                switch (thirdActionType)
                {
                    case 0: MoveTowardsTarget(actionNumber); break;
                    case (ActionType)1: TurnTowardsTarget(actionNumber); break;
                    case (ActionType)2: FireForward(actionNumber); break;
                    case (ActionType)3: StartCoroutine(WaitAndReturnCurrentStepTo1(actionNumber, 0.1f)); break;
                }
                break;

            case 4:
                switch (fourthActionType)
                {
                    case 0: MoveTowardsTarget(actionNumber); break;
                    case (ActionType)1: TurnTowardsTarget(actionNumber); break;
                    case (ActionType)2: FireForward(actionNumber); break;
                    case (ActionType)3: StartCoroutine(WaitAndReturnCurrentStepTo1(actionNumber, 0.1f)); break;
                }
                break;

            case 5:
                switch (fifthActionType)
                {
                    case 0: MoveTowardsTarget(actionNumber); break;
                    case (ActionType)1: TurnTowardsTarget(actionNumber); break;
                    case (ActionType)2: FireForward(actionNumber); break;
                    case (ActionType)3: StartCoroutine(WaitAndReturnCurrentStepTo1(actionNumber, 0.1f)); break;
                }
                break;

            case 6:
                switch (sixthActionType)
                {
                    case 0: MoveTowardsTarget(actionNumber); break;
                    case (ActionType)1: TurnTowardsTarget(actionNumber); break;
                    case (ActionType)2: FireForward(actionNumber); break;
                    case (ActionType)3: StartCoroutine(WaitAndReturnCurrentStepTo1(actionNumber, 0.1f)); break;
                }
                break;

            case 7:
                switch (seventhActionType)
                {
                    case 0: MoveTowardsTarget(actionNumber); break;
                    case (ActionType)1: TurnTowardsTarget(actionNumber); break;
                    case (ActionType)2: FireForward(actionNumber); break;
                    case (ActionType)3: StartCoroutine(WaitAndReturnCurrentStepTo1(actionNumber, 0.1f)); break;
                }
                break;

            case 8:
                switch (eighthActionType)
                {
                    case 0: MoveTowardsTarget(actionNumber); break;
                    case (ActionType)1: TurnTowardsTarget(actionNumber); break;
                    case (ActionType)2: FireForward(actionNumber); break;
                    case (ActionType)3: StartCoroutine(WaitAndReturnCurrentStepTo1(actionNumber, 0.1f)); break;
                }
                break;
        }
    }

    //Fires a bullet towards the direction the antagonist is facing.
    void FireForward(float step)
    {
        if (currentMovementStep == step)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            SetWaitTimeBasedOnStep();
            currentMovementStep += 0.5f;
            StartCoroutine(WaitAndMoveToNextStep(actionDuration));
        }

        void SetWaitTimeBasedOnStep()
        {
            if (step == 1) actionDuration = firstStepMovementDuration;
            if (step == 2) actionDuration = secondStepMovementDuration;
            if (step == 3) actionDuration = thirdStepMovementDuration;
            if (step == 4) actionDuration = fourthStepMovementDuration;
            if (step == 5) actionDuration = fifthStepMovementDuration;
            if (step == 6) actionDuration = sixthStepMovementDuration;
            if (step == 7) actionDuration = seventhStepMovementDuration;
            if (step == 8) actionDuration = eighthStepMovementDuration;
        }
    }

    //Moves the antagonist towards the target transform
    void MoveTowardsTarget(float step)
    {
        if (currentMovementStep == step)
        {
            SetVariablesBasedOnStep();
            currentMovementStep += 0.5f;
            StartCoroutine(CountMovementDuration(actionDuration));
        }
        if (step + 0.5f == currentMovementStep)
        {
            rb.MovePosition(rb.position + movementVelocity / actionDuration * Time.deltaTime);
        }

        IEnumerator CountMovementDuration(float duration)
        {
            movementVelocity = targetPosition - transform.position;
            yield return new WaitForSeconds(duration);
            currentMovementStep += 0.5f;
        }

        void SetVariablesBasedOnStep()
        {
            if (step == 1)
            {
                targetPosition = step1TargetTransform.position;
                actionDuration = firstStepMovementDuration;
            }
            if (step == 2)
            {
                targetPosition = step2TargetTransform.position;
                actionDuration = secondStepMovementDuration;
            }
            if (step == 3)
            {
                targetPosition = step3TargetTransform.position;
                actionDuration = thirdStepMovementDuration;
            }
            if (step == 4)
            {
                targetPosition = step4TargetTransform.position;
                actionDuration = fourthStepMovementDuration;
            }
            if (step == 5)
            {
                targetPosition = step5TargetTransform.position;
                actionDuration = fifthStepMovementDuration;
            }
            if (step == 6)
            {
                targetPosition = step6TargetTransform.position;
                actionDuration = sixthStepMovementDuration;
            }
            if (step == 7)
            {
                targetPosition = step7TargetTransform.position;
                actionDuration = seventhStepMovementDuration;
            }
            if (step == 8)
            {
                targetPosition = step8TargetTransform.position;
                actionDuration = eighthStepMovementDuration;
            }
        }
    }

    //Rotates the antagonist to face target position
    void TurnTowardsTarget(float step)
    {
        if (currentMovementStep == step)
        {
            SetVariablesBasedOnStep();
            currentMovementStep += 0.5f;
            StartCoroutine(CountTurnDuration(actionDuration));
        }
        if (step + 0.5f == currentMovementStep)
        {
            LookAtTarget();
        }

        IEnumerator CountTurnDuration(float duration)
        {
            yield return new WaitForSeconds(duration);
            currentMovementStep += 0.5f;
        }

        void LookAtTarget()
        {
            Vector3 relativePosition = targetPosition - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePosition);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }


        void SetVariablesBasedOnStep()
        {
            if (step == 1)
            {
                targetPosition = step1TargetTransform.position;
                actionDuration = firstStepMovementDuration;
            }
            if (step == 2)
            {
                targetPosition = step2TargetTransform.position;
                actionDuration = secondStepMovementDuration;
            }
            if (step == 3)
            {
                targetPosition = step3TargetTransform.position;
                actionDuration = thirdStepMovementDuration;
            }
            if (step == 4)
            {
                targetPosition = step4TargetTransform.position;
                actionDuration = fourthStepMovementDuration;
            }
            if (step == 5)
            {
                targetPosition = step5TargetTransform.position;
                actionDuration = fifthStepMovementDuration;
            }
            if (step == 6)
            {
                targetPosition = step6TargetTransform.position;
                actionDuration = sixthStepMovementDuration;
            }
            if (step == 7)
            {
                targetPosition = step7TargetTransform.position;
                actionDuration = seventhStepMovementDuration;
            }
            if (step == 8)
            {
                targetPosition = step8TargetTransform.position;
                actionDuration = eighthStepMovementDuration;
            }
        }
    }

    //waits for the time and goes back to action on step 1
    IEnumerator WaitAndReturnCurrentStepTo1(float step, float waitTime)
    {
        if (currentMovementStep == step)
        {
            currentMovementStep += 0.5f;
            yield return new WaitForSeconds(waitTime);
            currentMovementStep = 1;
        }
    }

    //waits for the time and moves to next action step
    IEnumerator WaitAndMoveToNextStep(float duration)
    {
        yield return new WaitForSeconds(duration);
        currentMovementStep += 0.5f;
    }

    //list of action steps, will be deleted after code refactoring
    enum ActionType { MoveTowardsTarget, TurnTowardsTarget, FireForward, WaitAndReturnCurrentStepTo1 }
}
