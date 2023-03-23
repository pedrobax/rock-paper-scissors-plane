using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    [SerializeReference]
    public List<GameObject> encounterList;
    public List<int> timeBetweenEncounters;
    public int currentEncounter = -1;
    bool isOnTimeBetweenEncounters;
    

    private void Update()
    {
        if (currentEncounter == -1) StartCoroutine(MoveToNextEncounter());
        if (currentEncounter >= encounterList.Count) Debug.Log("No more encounters!");
        else if (encounterList[currentEncounter].GetComponent<Encounter>().isEncounterDone == true && !isOnTimeBetweenEncounters)
        {
            StartCoroutine(MoveToNextEncounter());
        }
    }

    IEnumerator MoveToNextEncounter()
    {
        if (currentEncounter == -1)
        {
            currentEncounter++;
            encounterList[currentEncounter] = Instantiate(encounterList[currentEncounter]);
        }
        else
        {
            isOnTimeBetweenEncounters = true;
            yield return new WaitForSeconds(timeBetweenEncounters[currentEncounter]);
            currentEncounter++;
            encounterList[currentEncounter] = Instantiate(encounterList[currentEncounter]);
            isOnTimeBetweenEncounters = false;
        }
    }
}
