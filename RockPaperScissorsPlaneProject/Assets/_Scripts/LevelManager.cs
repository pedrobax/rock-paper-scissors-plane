using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int currentLevel;
    int currentScore;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void GoToLevel(Scene scene)
    {

    }
}
