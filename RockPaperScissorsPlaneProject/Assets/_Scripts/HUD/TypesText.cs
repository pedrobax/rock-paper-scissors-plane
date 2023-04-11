using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypesText : MonoBehaviour
{
   [SerializeField] public PlayerHealth playerHealth;
   [SerializeField] thisType type;

    private void Update()
    {
        if (playerHealth.currentType == PlayerHealth.PlayerType.Rock)
        {
            if (type == thisType.Rock) GetComponent<Text>().fontSize = 60;
            else GetComponent<Text>().fontSize = 40;
        }
        if (playerHealth.currentType == PlayerHealth.PlayerType.Paper)
        {
            if (type == thisType.Paper) GetComponent<Text>().fontSize = 60;
            else GetComponent<Text>().fontSize = 40;
        }
        if (playerHealth.currentType == PlayerHealth.PlayerType.Scissors)
        {
            if (type == thisType.Scissors) GetComponent<Text>().fontSize = 60;
            else GetComponent<Text>().fontSize = 40;
        }
    }

    public enum thisType
    {
        Rock,
        Paper,
        Scissors
    }
}
