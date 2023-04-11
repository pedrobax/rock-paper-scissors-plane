using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public float score;
    public float highscore;

    private void Awake()
    {
        GameManager.ScoreUpdated += AddToScore;
    }
    void Start()
    {
        score = 0;
        highscore = PlayerPrefs.GetInt("highscore", 0);
    }

    void AddToScore(float scoreToAdd)
    {
        score += scoreToAdd;
        PlayerPrefs.SetInt("score", (int)score);
        if (highscore < score)
        {
            PlayerPrefs.SetInt("highscore", (int)score);
        }
    }

    private void OnDestroy()
    {
        GameManager.ScoreUpdated -= AddToScore;
    }
}
