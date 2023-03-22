using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestSpawner))]
public class TestSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TestSpawner TestSpawner = (TestSpawner)target;

        if (GUILayout.Button("Spawn Enemy"))
        {
            TestSpawner.SpawnEnemy();
        }
    }
}
