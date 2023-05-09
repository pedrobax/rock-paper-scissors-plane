using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterManager : MonoBehaviour
{
    [SerializeReference]
    public List<GameObject> encounterList;
    public List<int> timeBetweenEncounters; //this is the time that the encounter manager will wait
                                            //before starting the next encounter at the end of an encounter
    public int currentEncounter = -1;
    bool isOnTimeBetweenEncounters;

    public Transform overText; //this is used for debugging and will be removed in the final version

    private void Update()
    {
        if (currentEncounter == -1) StartCoroutine(MoveToNextEncounter()); //starts the first encounter, which is 0
        if (currentEncounter >= encounterList.Count || Input.GetKeyDown(KeyCode.F7)) //if there are no more encounters this will end the exam,
                                                                                     //F7 keycode used for testing cheats and skipping exam
        {
            GameManager.FinishExam();
            Destroy(gameObject);
        }
        //checks if current encounter is done and if it is, starts the next one
        else if (encounterList[currentEncounter].GetComponent<Encounter>().isEncounterDone == true && !isOnTimeBetweenEncounters)
        {
            StartCoroutine(MoveToNextEncounter());
        }
        if (encounterList[currentEncounter] == null && currentEncounter >= encounterList.Count)
        {
            SceneManager.LoadScene(2);
        }
    }

    //Spawns the next encounter object, but waits before doing so if there is a time between encounters
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
