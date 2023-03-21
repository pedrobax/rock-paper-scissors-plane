using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ActionList))]
public class AntagonistEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ActionList actionList = (ActionList)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Linear Movement"))
        {
            actionList.AddLinearMovementAction();
        }

        if (GUILayout.Button("Ease In Movement"))
        {
            actionList.AddEaseInMovementAction();
        }

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Turn Towards Target"))
        {
            actionList.AddTurnTowardsTargetAction();
        }


        if (GUILayout.Button("Turn Towards Player"))
        {
            actionList.AddTurnTowardsPlayerAction();
        }

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Shoot Forward"))
        {
            actionList.AddShootForwardAction();
        }

        if (GUILayout.Button("Self Destruct"))
        {
            actionList.AddSelfDestructAction();
        }

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Go Back to Action X"))
        {
            actionList.AddGoBackToActionXAction();
        }

        if (GUILayout.Button("Parabolic Movement"))
        {
            actionList.AddParabolicMovementAction();
        }

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Orbit Around Target Y Clockwise"))
        {
            actionList.AddOrbitAroundTargetYClockwiseAction();
        }

        if (GUILayout.Button("Orbit Around Target Y Anti Clockwise"))
        {
            actionList.AddOrbitAroundTargetYAntiClockwiseAction();
        }

        if (GUILayout.Button("Accelerate Towards Player"))
        {
            actionList.AddAccelerateTowardsPlayerAction();
        }

        if (GUILayout.Button("Clear Lists"))
        {
            actionList.ClearLists();
        }
    }
}
