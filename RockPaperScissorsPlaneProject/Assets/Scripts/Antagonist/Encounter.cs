using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    [SerializeReference]
    public List<GameObject> enemyList;
    public bool isEncounterDone = false;
    public int currentEnemyNumber;
    public float maxDuration = 30;
    public float currentDuration = 0;

    private void Update()
    {
        CountDuration();
        if (currentDuration > maxDuration) ClearEncounter();
        currentEnemyNumber = enemyList.Count;
        CheckEnemies();
        if (currentEnemyNumber == 0) FinishEncounter();
    }

    void ClearEncounter()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            Destroy(enemyList[i]);
        }
    }

    void CountDuration()
    {
        currentDuration += Time.deltaTime;
    }

    void CheckEnemies()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null) enemyList.Remove(enemyList[i]);
        }
    }

    void FinishEncounter()
    {
        isEncounterDone = true;
    }
}
