using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ActionList : MonoBehaviour
{
    [SerializeReference]
    public List<Action> actionList; //= new List<Action>();

    [SerializeReference]
    public List<GameObject> targetList; //= new List<GameObject>();

    [SerializeReference]
    public List<GameObject> secondaryTargetList; //= new List<GameObject>();

    public int currentAction = 0;
    [SerializeField] GameObject antagonistTargetPrefab;

    public int listsSize;
    public int secondaryTargetListSize;

    //[SerializeField] bool autoSelect = false;

    private void Update()
    {
        if (currentAction == actionList.Count)
        {
            for (int i = 0; i < actionList.Count; i++)
            {
                actionList[i].hasActed = false;
            }
            currentAction = 1; //0 should always be a spawn action
        }
        actionList[currentAction].Act();
        if (actionList[currentAction].isActing && currentAction > 0)
        {
            actionList[currentAction - 1].hasActed = false;
        }
        if (actionList[currentAction].hasActed)
        {
            currentAction++;
        }
    }


    /*private void OnDrawGizmos()
    {
        for(int i = 0;i < actionList.Count;i++)
        {
            if (actionList[i].GetType() == typeof(LinearMovementAction) || actionList[i].GetType() == typeof(EaseInMovementAction))
            {
                Gizmos.color = Color.blue;
                if (i == 0)
                {
                    Gizmos.DrawLine(this.transform.position, targetList[0].transform.position);
                }
                else
                {
                    for (int o = 1; o <= actionList.Count - 1; o++)
                    {
                        if (actionList[i-o].GetType() == typeof(LinearMovementAction) || actionList[i-o].GetType() == typeof(EaseInMovementAction))
                        {
                            Gizmos.DrawLine(targetList[i].transform.position, targetList[i-o].transform.position);
                            break;
                        }
                    }
                }
            }
            if (actionList[i].GetType() == typeof(TurnTowardsTargetAction))
            {
                Gizmos.color = Color.yellow;
                if (i == 0)
                {
                    Gizmos.DrawLine(this.transform.position, targetList[0].transform.position);
                }
                else if (actionList[i - 1].GetType() == typeof(LinearMovementAction) || actionList[i - 1].GetType() == typeof(EaseInMovementAction))
                {
                    Gizmos.DrawLine(targetList[i].transform.position, targetList[i - 1].transform.position);
                }
                else
                {
                    for (int o = 1; o < actionList.Count - 1; o++)
                    {
                        if (actionList[i - o].GetType() == typeof(LinearMovementAction) || actionList[i - o].GetType() == typeof(EaseInMovementAction))
                        {
                            Gizmos.DrawLine(targetList[i - o].transform.position, targetList[i].transform.position);
                            break;
                        }
                    }
                }
            }

            if (actionList[i].GetType() == typeof(TurnTowardsPlayerAction))
            {
                Gizmos.color = Color.yellow;
                if (i == 0)
                {
                    Gizmos.DrawLine(this.transform.position, GameManager.GetPlayerPosition());
                }
                else if (actionList[i - 1].GetType() == typeof(LinearMovementAction) || actionList[i - 1].GetType() == typeof(EaseInMovementAction))
                {
                    Gizmos.DrawLine(GameManager.GetPlayerPosition(), targetList[i - 1].transform.position);
                }
                else
                {
                    for (int o = 1; o < actionList.Count - 1; o++)
                    {
                        if (actionList[i - o].GetType() == typeof(LinearMovementAction) || actionList[i - o].GetType() == typeof(EaseInMovementAction))
                        {
                            Gizmos.DrawLine(targetList[i - o].transform.position, GameManager.GetPlayerPosition());
                            break;
                        }
                    }
                }
            }

            if (actionList[i].GetType() == typeof(ShootForwardAction))
            {
                Gizmos.color = Color.red;
                if (i == 0)
                {

                }
                else if (actionList[i - 1].GetType() == typeof(LinearMovementAction) || actionList[i - 1].GetType() == typeof(EaseInMovementAction))
                {
                    Gizmos.DrawLine(targetList[i].transform.position, targetList[i - 1].transform.position);
                }
                else
                {
                    for (int o = 1; o < actionList.Count - 1; o++)
                    {
                        if (actionList[i - o].GetType() == typeof(LinearMovementAction) || actionList[i - o].GetType() == typeof(EaseInMovementAction))
                        {
                            Gizmos.DrawLine(targetList[i - o].transform.position, targetList[i].transform.position);
                            break;
                        }
                    }
                }
            }
        }
    } */

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

    public void CreateTarget()
    {
        listsSize = actionList.Count;
        targetList.Add(Instantiate(antagonistTargetPrefab));
        targetList[listsSize].name = "Target " + listsSize;
    }

    public void CreateSecondaryTarget()
    {
        secondaryTargetListSize = secondaryTargetList.Count;
        secondaryTargetList.Add(Instantiate(antagonistTargetPrefab));
        secondaryTargetList[secondaryTargetListSize].name = "Secondary Target " + listsSize;
    }

    public void SetTarget()
    {
        actionList[listsSize].targetTransform = targetList[listsSize].transform;
        targetList[listsSize].transform.parent = this.transform.parent;
    }

    public void SetSecondaryTarget()
    {
        actionList[listsSize].secondaryTargetTransform = secondaryTargetList[secondaryTargetListSize].transform;
        secondaryTargetList[secondaryTargetListSize].transform.parent = this.transform.parent;
    }

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
