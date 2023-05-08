using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterManager : MonoBehaviour
{
    [SerializeReference]
    public List<GameObject> encounterList;
    public List<int> timeBetweenEncounters;
    public int currentEncounter = -1;
    bool isOnTimeBetweenEncounters;

    public Transform overText;

    private void Update()
    {
        if (currentEncounter == -1) StartCoroutine(MoveToNextEncounter());
        if (currentEncounter >= encounterList.Count || Input.GetKeyDown(KeyCode.F7))
        {
            GameManager.FinishExam();
            Destroy(gameObject);
        }
        else if (encounterList[currentEncounter].GetComponent<Encounter>().isEncounterDone == true && !isOnTimeBetweenEncounters)
        {
            StartCoroutine(MoveToNextEncounter());
        }
        if (encounterList[currentEncounter] == null && currentEncounter >= encounterList.Count)
        {
            SceneManager.LoadScene(2);
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
