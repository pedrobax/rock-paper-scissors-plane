using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextHighScore : MonoBehaviour
{
    public TMP_Text highScore;
    void Start()
    {
        highScore = GetComponent<TMP_Text>();
    }

    void Update()
    {
        highScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt("highscore", 0);
    }
}
