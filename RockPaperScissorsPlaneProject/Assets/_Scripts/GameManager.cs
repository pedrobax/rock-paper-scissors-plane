using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //to make this a Singleton

    public GameObject player; //stores reference to the player object for all to access
    private PlayerHealth playerHealth; //^
    public LevelArea _levelArea; //stores reference to player area
    public Transform _playerTransform; //stores reference to the player transform for all to access
    static float activationArea; //TODO possibily remove this, it is not used with the new Antagonist System
    static Vector3 playerPosition; //stores the player's position for all to access TODO check if this is used
    public GameObject pauseMenu; //stores reference to the pause menu UI object
    public GameObject deathMenu; //stores reference to the death menu UI object
    public static PlayerType currentPlayerType; //stores the current player's type for all to access
    public static float currentPlayerHealth; //stores the current player's health for all to access

    public AudioMixer audioMixer; //audioMixer reference used to set saved volume from past sessions

    public GameObject examCompletedScreenObject; //stores reference to the exam completed screen UI object
    public static GameObject examCompletedScreen; //TODO check what is the difference between this and the above and if both are needed

    public static float examScore; //score made in the current exam
    public static float overallScore; //score from the entire game play session
    public static float highscore; //highest score ever achieved should be stored here
    public static float enemiesDefeated; //enemies defeated in TODO check if this is only in last exam or all the game

    public static int currentExam = 0;
    public static int currentLevel = 0;

    public LevelType levelType = LevelType.First;

    public static ExamListReader examList; //will be used to read below
    public ExamList examListScriptableObject;

    private void Awake()
    {
        Instance = this;
        examList = GetComponent<ExamListReader>();
        SetSavedVolume(); //sets saved volume from player prefs to the audio mixer
    }

    private void Start()
    {
        //gathers info from exam list scriptable object
        examList.exams = examListScriptableObject.exams;
        examList.maxScore = examListScriptableObject.maxScore;

        //stores the completed screen to the static variable TODO check why this is needed
        examCompletedScreen = examCompletedScreenObject;
        activationArea = _levelArea.verticalAreaLimit * 2; //TODO check if this is needed and delete it if not
        playerHealth = player.GetComponent<PlayerHealth>(); //stores player health
        Time.timeScale = 1f; //sets timescale to prevent slow mo bugs
        examScore = 0; //resets exam score
        if (levelType == LevelType.First) overallScore = 0; //resets overall score if starting the first level
        if (levelType == LevelType.First) currentExam = 0;  //resets current exam if starting the first level
        currentLevel = 1; //TODO rework this
        highscore = PlayerPrefs.GetInt("highscore", 0); //gets highscore from player prefs
        PlayerPrefs.SetInt("examScore", 0); //resets exam score in player prefs
        PlayerPrefs.SetInt("enemiesDefeated", 0); //resets enemies defeated in player prefs
        Instantiate(examList.exams[currentExam], transform.position, transform.rotation);  //TODO change this to a modifiable number, so we can work with multiple GameManagers in multiple scenes
    }

    private void OnDrawGizmos()
    {
        playerPosition = _playerTransform.position; //TODO check what this is used for
    }

    private void Update()
    {
        playerPosition = _playerTransform.position; //saves current player position each frame, TODO check if this is needed

        //checks if player is dead and activates death menu
        if (playerHealth.isAlive == false)
        {
            deathMenu.SetActive(true);
        }

        //pauses game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        //stores current player type for referencing, TODO check where this is needed
        if (playerHealth.currentType == PlayerHealth.PlayerType.Rock) SetCurrentPlayerType(PlayerType.Rock);
        if (playerHealth.currentType == PlayerHealth.PlayerType.Paper) SetCurrentPlayerType(PlayerType.Paper);
        if (playerHealth.currentType == PlayerHealth.PlayerType.Scissors) SetCurrentPlayerType(PlayerType.Scissors);

        //stores current player hp, TODO check if this is needed
        currentPlayerHealth = playerHealth.lives;
    }

    // resets examScore for a new Level/Exam and sets the playerprefs int to the new value
    // TODO check if the player prefs is just used to prevent UI bugs
    public static void StartLevel()
    {
        examScore = 0;
        PlayerPrefs.SetInt("examScore", (int)examScore);
    }

    //Starts the exam set in the current exam variable, should have just ended another exam
    public static void StartExam()
    {
        //TODO refactor this hard coded disgrace
        if(currentExam == 2 && currentLevel == 1)
        {
            GoToNextLevel();
        }
        else if (currentExam < examList.exams.Count) //starts a new exam if there are more exams to be played
        {
            examScore = 0;
            PlayerPrefs.SetInt("examScore", (int)examScore);
            enemiesDefeated = 0;
            PlayerPrefs.SetInt("enemiesDefeated", (int)enemiesDefeated);
            Instantiate(examList.exams[currentExam]);
        }      
        else //if there aren't goes to final screen
        {
            GoToFinalScreen();
        }
    }

    //sets the examCompleted screen to active and it does the rest
    public static void FinishExam()
    {
        examCompletedScreen.SetActive(true);
    }

    //adds score to the exam score and overall score, also adds 1 to enemies defeated and saves all to player prefs
    //called when enemies die
    public static void UpdateScore(float scoreToAdd)
    {
        examScore += scoreToAdd;
        overallScore += scoreToAdd;
        enemiesDefeated++;
        PlayerPrefs.SetInt("examScore", (int)examScore);
        PlayerPrefs.SetInt("overallScore", (int)overallScore);
        PlayerPrefs.SetInt("enemiesDefeated", (int)enemiesDefeated);
        if (highscore < overallScore)
        {
            PlayerPrefs.SetInt("highscore", (int)overallScore);
        }
    }

    //shakes screen by the intensity for the duration
    public static void ShakeScreen(float intensity, float duration)
    {
        CinemachineShake.Instance.ShakeCamera(intensity, duration);
    }

    //TODO check where this is used
    private static void SetCurrentPlayerType(PlayerType playerType)
    {
        currentPlayerType = playerType;
    }

    //TODO change this hard coded mess that also doesn't work
    static void GoToNextLevel()
    {
        SceneManager.LoadScene(2);
        currentLevel++;
        StartExam();
    }

    //loads the final screen and ends the game
    static void GoToFinalScreen()
    {
        SceneManager.LoadScene(3);
    }

    //returns the current player type for referencing
    public static PlayerType GetCurrentPlayerType()
    {
        return currentPlayerType;
    }

    //sets the volume of the audio mixer to the saved volume from last sessions
    public void SetSavedVolume()
    {
        audioMixer.SetFloat("MasterVol", Mathf.Log10(PlayerPrefs.GetFloat("MasterVol", 1f)) * 20);
        audioMixer.SetFloat("MusicVol", Mathf.Log10(PlayerPrefs.GetFloat("MusicVol", 1f)) * 20);
        audioMixer.SetFloat("SFXVol", Mathf.Log10(PlayerPrefs.GetFloat("SFXVol", 1f)) * 20);
    }

    //TODO check where this is used, probably delete it
    public static float GetActivationArea()
    {
        return activationArea;
    }

    //TODO check where this is used, probably delete it
    public static Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }

    public static float GetPlayerHealth()
    {
        return currentPlayerHealth;
    }

    public enum PlayerType { Rock, Paper, Scissors}

    public enum LevelType { First, Other }
} 
