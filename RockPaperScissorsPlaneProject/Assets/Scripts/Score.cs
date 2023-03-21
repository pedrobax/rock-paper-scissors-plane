using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public float score;

    private void Awake()
    {
        GameManager.ScoreUpdated += AddToScore;
    }
    void Start()
    {
        score = 0;
    }

    void AddToScore(float scoreToAdd)
    {
        score += scoreToAdd;
    }

    private void OnDestroy()
    {
        GameManager.ScoreUpdated -= AddToScore;
    }
}
