using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    [SerializeReference]
    public List<GameObject> enemyList;
    public bool isEncounterDone = false;
    public int currentEnemyNumber;

    private void Update()
    {
        currentEnemyNumber = enemyList.Count;
        CheckEnemies();
        if (currentEnemyNumber == 0) FinishEncounter();
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
