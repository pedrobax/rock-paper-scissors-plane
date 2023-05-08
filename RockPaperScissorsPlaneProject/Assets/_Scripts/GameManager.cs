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
    public static GameManager Instance;

    public GameObject player;
    private PlayerHealth playerHealth;
    public LevelArea _levelArea;
    public Transform _playerTransform;
    static float activationArea;
    static Vector3 playerPosition;
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public static PlayerType currentPlayerType;
    public static float currentPlayerHealth;

    public AudioMixer audioMixer;

    public GameObject examCompletedScreenObject;
    public static GameObject examCompletedScreen;

    public static float examScore;
    public static float overallScore;
    public static float highscore;
    public static float enemiesDefeated;

    public static int currentExam = 0;
    public static int currentLevel = 0;

    public static ExamListReader examList;
    public ExamList examListScriptableObject;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        examList = GetComponent<ExamListReader>();
        SetSavedVolume();
    }

    private void Start()
    {
        examList.exams = examListScriptableObject.exams;
        examList.maxScore = examListScriptableObject.maxScore;
        examCompletedScreen = examCompletedScreenObject;
        activationArea = _levelArea.verticalAreaLimit * 2;
        Debug.Log("Activation Area: " + activationArea);
        playerHealth = player.GetComponent<PlayerHealth>();
        Time.timeScale = 1f;
        examScore = 0;
        overallScore = 0;
        currentExam = 0;
        currentLevel = 1;
        highscore = PlayerPrefs.GetInt("highscore", 0);
        PlayerPrefs.SetInt("examScore", 0);
        PlayerPrefs.SetInt("enemiesDefeated", 0);
        Instantiate(examList.exams[0], transform.position, transform.rotation); 
    }

    private void OnDrawGizmos()
    {
        playerPosition = _playerTransform.position;
    }

    private void Update()
    {
        playerPosition = _playerTransform.position;
        if (playerHealth.isAlive == false)
        {
            deathMenu.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        if (playerHealth.currentType == PlayerHealth.PlayerType.Rock) SetCurrentPlayerType(PlayerType.Rock);
        if (playerHealth.currentType == PlayerHealth.PlayerType.Paper) SetCurrentPlayerType(PlayerType.Paper);
        if (playerHealth.currentType == PlayerHealth.PlayerType.Scissors) SetCurrentPlayerType(PlayerType.Scissors);

        currentPlayerHealth = playerHealth.lives;
    }

    public static void StartLevel()
    {
        examScore = 0;
        PlayerPrefs.SetInt("examScore", (int)examScore);
    }

    public static void StartExam()
    {
        if (currentExam < examList.exams.Count)
        {
            examScore = 0;
            PlayerPrefs.SetInt("examScore", (int)examScore);
            enemiesDefeated = 0;
            PlayerPrefs.SetInt("enemiesDefeated", (int)enemiesDefeated);
            Instantiate(examList.exams[currentExam]);
        }      
        else if (currentLevel == 1)
        {
            Debug.Log("Level over, going to next level");
            GoToNextLevel();
        }
        else
        {
            Debug.Log("Exams over, going to end screen");
            GoToFinalScreen();
        }
    }

    public static void FinishExam()
    {
        examCompletedScreen.SetActive(true);
    }

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

    public static void ShakeScreen(float intensity, float duration)
    {
        CinemachineShake.Instance.ShakeCamera(intensity, duration);
    }

    private static void SetCurrentPlayerType(PlayerType playerType)
    {
        currentPlayerType = playerType;
    }

    static void GoToNextLevel()
    {
        SceneManager.LoadScene(2);
        currentLevel++;
        currentExam++;
        StartExam();
    }

    static void GoToFinalScreen()
    {
        SceneManager.LoadScene(3);
    }

    public static PlayerType GetCurrentPlayerType()
    {
        return currentPlayerType;
    }

    public void SetSavedVolume()
    {
        audioMixer.SetFloat("MasterVol", Mathf.Log10(PlayerPrefs.GetFloat("MasterVol", 1f)) * 20);
        audioMixer.SetFloat("MusicVol", Mathf.Log10(PlayerPrefs.GetFloat("MusicVol", 1f)) * 20);
        audioMixer.SetFloat("SFXVol", Mathf.Log10(PlayerPrefs.GetFloat("SFXVol", 1f)) * 20);
    }

    public static float GetActivationArea()
    {
        return activationArea;
    }

    public static Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }

    public static float GetPlayerHealth()
    {
        return currentPlayerHealth;
    }

    public enum PlayerType { Rock, Paper, Scissors}
} 
