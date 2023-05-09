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
    public bool clearAtEnd = false; //decides if the encounter will clear all enemies at the end or just end the encounter

    public List<float> spawnTimes; //list of spawn times for each enemy in the encounter, this is used in the editor
                                   //for fast iteration

    public float setWaitTime = 3;  //this is the time the enemy will spend moving in the spawn action, not the actual waitTime,
                                   //the naming is a bit confusing

    private void Update()
    {
        CountDuration(); //counts the current duration of the encounter
        if (currentDuration > maxDuration) ClearEncounter(); 
        currentEnemyNumber = enemyList.Count; 
        CheckEnemies(); //checks if the enemies are still alive, if not removes them from the list
        if (currentEnemyNumber == 0) FinishEncounter(); //if there are no enemies left, the encounter is finished
    }

    //if clearAtEnd is true, this will destroy all enemies in the encounter' end, otherwise it will
    //just finish the encounter and overlap the current enemies with the ones in the next encounter
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
    
    //this is used in the Editor to add all enemies in the encounter to the list rapidly
    //adds all the children of the encounter object to the list
    public void AddEnemiesToList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            enemyList.Add(transform.GetChild(i).gameObject);
        }
    }

    //this is used in the editor to set the spawn times of the enemies in the encounter
    //based on the list with the inputs of the user
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
