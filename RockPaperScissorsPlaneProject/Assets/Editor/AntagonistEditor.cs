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

        if (GUILayout.Button("Linear Movement"))
        {
            actionList.AddLinearMovementAction();
        }

        if (GUILayout.Button("Linear Movement Ease"))
        {
            actionList.AddLinearMovementEaseAction();
        }

        if (GUILayout.Button("Turn Towards Target"))
        {
            actionList.AddTurnTowardsTargetAction();
        }


        if (GUILayout.Button("Turn Towards Player"))
        {
            actionList.AddTurnTowardsPlayerAction();
        }

        if (GUILayout.Button("Shoot Forward"))
        {
            actionList.AddShootForwardAction();
        }

        if (GUILayout.Button("Self Destruct"))
        {
            actionList.AddSelfDestructAction();
        }

        if (GUILayout.Button("Go Back to Action X"))
        {
            actionList.AddGoBackToActionXAction();
        }

        if (GUILayout.Button("Parabolic Movement"))
        {
            actionList.AddParabolicMovementAction();
        }

        if (GUILayout.Button("Parabolic Movement Ease"))
        {
            actionList.AddParabolicMovementEaseAction();
        }

        if (GUILayout.Button("Orbit Around Target Y Clockwise"))
        {
            actionList.AddOrbitAroundTargetYClockwiseAction();
        }

        if (GUILayout.Button("Orbit Around Target Y Clockwise Fixed Distance"))
        {
            actionList.AddOrbitAroundTargetYClockwiseFixedDistanceAction();
        }

        if (GUILayout.Button("Orbit Around Target Y Anti Clockwise"))
        {
            actionList.AddOrbitAroundTargetYAntiClockwiseAction();
        }

        if (GUILayout.Button("Accelerate Towards Player"))
        {
            actionList.AddAccelerateTowardsPlayerAction();
        }

        if (GUILayout.Button("Shoot Barrage Forward"))
        {
            actionList.AddShootBarrageForwardAction();
        }

        if (GUILayout.Button("Rotate In Place"))
        {
            actionList.AddRotateInPlaceAction();
        }

        if (GUILayout.Button("Spawn"))
        {
            actionList.AddSpawnAction();
        }

        if (GUILayout.Button("Clear Lists"))
        {
            actionList.ClearLists();
        }
    }
}
