using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Encounter))]
public class EncounterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Encounter encounter = (Encounter)target;

        if (GUILayout.Button("Add All Children to List"))
        {
            encounter.AddEnemiesToList();
        }

        if (GUILayout.Button("Set Spawn Times"))
        {
            encounter.SetSpawnTimes();
        }
    }
}
