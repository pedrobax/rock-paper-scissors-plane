using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ActionList : MonoBehaviour
{
    [SerializeReference]
    public List<Action> actionList; //stores all the actions to be performed linearly by the antagonist

    [SerializeReference]
    public List<GameObject> targetList; //stores all the targets for the actions in the same index

    [SerializeReference]
    public List<GameObject> secondaryTargetList; //stores all the secondary targets for the actions,
                                                 //list doesn't keep the same amount, so indexes won't be connected

    public int currentAction = 0; //the index of the action currently being performed
    [SerializeField] GameObject antagonistTargetPrefab; //prefab for the empty target gameobject

    public int listsSize;
    public int secondaryTargetListSize;

    //[SerializeField] bool autoSelect = false;

    private void Update()
    { 
        //resets the action list's acted states when it reaches the end so it can loop
        if (currentAction == actionList.Count)
        {
            for (int i = 0; i < actionList.Count; i++)
            {
                actionList[i].hasActed = false;
            }
            currentAction = 1; //0 should always be a spawn action, we ignore it in the loop
        }

        actionList[currentAction].Act(); //performs the current action

        //if the current action has been performed, it sets the previous action's hasActed to false
        //this enables it to work again if the action list loops before its end, usually happens via a GoBackToActionXAction
        if (actionList[currentAction].isActing && currentAction > 0)
        {
            actionList[currentAction - 1].hasActed = false;
        }
        //moves to next action if the current one has been performed
        if (actionList[currentAction].hasActed)
        {
            currentAction++;
        }
    }

    //all ADDAction are used by the inspector to add existing actions to the list when level building
    public void AddLinearMovementAction()
    {  
        CreateTarget();
        //if(autoSelect == true) Selection.activeGameObject = targetList[listsSize];
        actionList.Add(this.gameObject.AddComponent<LinearMovementAction>());
        SetTarget();
    }

    public void AddLinearMovementEaseAction()
    {
        CreateTarget();
        //if (autoSelect == true) Selection.activeGameObject = targetList[listsSize];
        actionList.Add(this.gameObject.AddComponent<LinearMovementEaseAction>());
        SetTarget();
    }

    public void AddParabolicMovementAction()
    {
        CreateTarget();
        CreateSecondaryTarget();
        actionList.Add(this.gameObject.AddComponent<ParabolicMovementAction>());
        SetTarget();
        SetSecondaryTarget();
    }

    public void AddParabolicMovementEaseAction()
    {
        CreateTarget();
        CreateSecondaryTarget();
        actionList.Add(this.gameObject.AddComponent<ParabolicMovementEaseAction>());
        SetTarget();
        SetSecondaryTarget();
    }

    public void AddOrbitAroundTargetYClockwiseAction()
    {
        CreateTarget();
        //if (autoSelect == true) Selection.activeGameObject = targetList[listsSize];
        actionList.Add(this.gameObject.AddComponent<OrbitAroundTargetYClockwiseAction>());
        SetTarget();
    }

    public void AddOrbitAroundTargetYClockwiseFixedDistanceAction()
    {
        CreateTarget();
        //if (autoSelect == true) Selection.activeGameObject = targetList[listsSize];
        actionList.Add(this.gameObject.AddComponent<OrbitateAroundTargetXClockwiseFixedDistanceAction>());
        SetTarget();
    }

    public void AddOrbitAroundTargetYAntiClockwiseAction()
    {
        CreateTarget();
        //if (autoSelect == true) Selection.activeGameObject = targetList[listsSize];
        actionList.Add(this.gameObject.AddComponent<OrbitAroundTargetYAntiClockwiseAction>());
        SetTarget();
    }

    public void AddAccelerateTowardsPlayerAction()
    {
        CreateTarget();
        actionList.Add(this.gameObject.AddComponent<AccelerateTowardsPlayerAction>());
        SetTarget();
    }

    public void AddTurnTowardsTargetAction()
    {
        CreateTarget();
        //if (autoSelect == true) Selection.activeGameObject = targetList[listsSize];
        actionList.Add(this.gameObject.AddComponent<TurnTowardsTargetAction>());
        SetTarget();
    }

    public void AddTurnTowardsPlayerAction()
    {
        CreateTarget();
        actionList.Add(this.gameObject.AddComponent<TurnTowardsPlayerAction>());
        SetTarget();
    }

    public void AddShootForwardAction()
    {
        CreateTarget();
        actionList.Add(this.gameObject.AddComponent<ShootForwardAction>());
        SetTarget();
    }

    public void AddShootBarrageForwardAction()
    {
        CreateTarget();
        actionList.Add(this.gameObject.AddComponent<ShootBarrageForwardAction>());
        SetTarget();
    }

    public void AddSelfDestructAction()
    {
        CreateTarget();
        actionList.Add(this.gameObject.AddComponent<SelfDestructAction>());
        SetTarget();
    }

    public void AddGoBackToActionXAction()
    {
        CreateTarget();
        actionList.Add(this.gameObject.AddComponent<GoBackToActionXAction>());
        SetTarget();
    }

    public void AddRotateInPlaceAction()
    {
        CreateTarget();
        actionList.Add(this.gameObject.AddComponent<RotateInPlaceAction>());
        SetTarget();
    }

    public void AddSpawnAction()
    {
        CreateTarget();
        actionList.Add(this.gameObject.AddComponent<SpawnAction>());
        SetTarget();
    }

    public void AddSpawnEaseAction()
    {
        CreateTarget();
        actionList.Add(this.gameObject.AddComponent<SpawnEaseAction>());
        SetTarget();
    }

    //creates a target in the scene and adds it to the target list for index referencing
    public void CreateTarget()
    {
        listsSize = actionList.Count;
        targetList.Add(Instantiate(antagonistTargetPrefab));
        targetList[listsSize].name = "Target " + listsSize;
    }

    //creates a secondary target in the scene and adds it to the target list for index referencing
    public void CreateSecondaryTarget()
    {
        secondaryTargetListSize = secondaryTargetList.Count;
        secondaryTargetList.Add(Instantiate(antagonistTargetPrefab));
        secondaryTargetList[secondaryTargetListSize].name = "Secondary Target " + listsSize;
    }

    //sets the target of the current highest indexed action to the target in the target list with the same index
    public void SetTarget()
    {
        actionList[listsSize].targetTransform = targetList[listsSize].transform;
        targetList[listsSize].transform.parent = this.transform.parent;
    }

    //does the same as above but uses a different number, since the secondary target list is a different size
    public void SetSecondaryTarget()
    {
        actionList[listsSize].secondaryTargetTransform = secondaryTargetList[secondaryTargetListSize].transform;
        secondaryTargetList[secondaryTargetListSize].transform.parent = this.transform.parent;
    }

    //has a button on the inspector to clear/reset the lists
    //makes life faster, brighter, happier, makes you really believe in the future of the human species
    public void ClearLists()
    {
        for (int i = 0; i < actionList.Count; i++)
        {
            DestroyImmediate(actionList[i]);
        }
        for (int i = 0; i < targetList.Count; i++)
        {
            DestroyImmediate(targetList[i]);
        }
        for (int i = 0; i < secondaryTargetList.Count; i++)
        {
            DestroyImmediate(secondaryTargetList[i]);
        }
        listsSize = 0;
        secondaryTargetListSize = 0;
        targetList.Clear();
        actionList.Clear();
        secondaryTargetList.Clear();
    }


}
