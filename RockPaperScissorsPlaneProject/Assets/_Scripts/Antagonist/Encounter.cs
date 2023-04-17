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
    public bool clearAtEnd = false;

    public List<float> spawnTimes;
    public float setWaitTime = 3;

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
        if (clearAtEnd == true)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                Destroy(enemyList[i]);
            }
        }
        else
        {
            FinishEncounter();
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
    
    public void AddEnemiesToList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            enemyList.Add(transform.GetChild(i).gameObject);
        }
    }

    public void SetSpawnTimes()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].GetComponentInChildren<SpawnEaseAction>().duration = spawnTimes[i];
            if (spawnTimes[i] - setWaitTime >= 0) enemyList[i].GetComponentInChildren<SpawnEaseAction>().waitTime = spawnTimes[i] - setWaitTime;
            else enemyList[i].GetComponentInChildren<SpawnEaseAction>().waitTime = 0;

        }
    }
}
