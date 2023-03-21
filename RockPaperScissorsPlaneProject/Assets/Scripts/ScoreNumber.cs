using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreNumber : MonoBehaviour
{
    public Text scoreTextNumber;
    public Score score;

    void Update()
    {
        scoreTextNumber.text = score.score.ToString();
    }
}
