using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AntagonistAnimation))]
public class AntagonistAnimationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AntagonistAnimation antagonistAnimation = (AntagonistAnimation)target;

        if (GUILayout.Button("Add Animation"))
        {
            antagonistAnimation.AddAnimation();
        }

        if (GUILayout.Button("Copy Action List"))
        {
            antagonistAnimation.GetActionList();
        }      
    }
}
