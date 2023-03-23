using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    [SerializeReference]
    public List<GameObject> enemyList;
    public bool isEncounterDone = false;

    void FinishEncounter()
    {
        isEncounterDone = true;
    }
}
