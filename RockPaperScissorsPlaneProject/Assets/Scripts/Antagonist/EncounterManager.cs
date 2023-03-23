using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    [SerializeReference]
    public List<GameObject> encounterList;
    public int currentEncounter = 0;
}
