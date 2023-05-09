using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//class used to display the current score on the UI
public class TextCurrentScore : MonoBehaviour
{
    public TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.text = "CURRENT SCORE: " + PlayerPrefs.GetInt("overallScore", 0); //displays the current score
    }
}
