using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event Action<float> ScoreUpdated;
    public GameObject player;
    private PlayerHealth playerHealth;
    public LevelArea _levelArea;
    public Transform _playerTransform;
    static float activationArea;
    static Vector3 playerPosition;
    public GameObject pauseMenu;
    public GameObject deathMenu;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        activationArea = _levelArea.verticalAreaLimit * 2;
        Debug.Log("Activation Area: " + activationArea);
        playerHealth = player.GetComponent<PlayerHealth>();
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("score", 0);
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
    }

    public static void UpdateScore(float score)
    {   
        ScoreUpdated?.Invoke(score);
    }

    public static void ShakeScreen(float intensity, float duration)
    {
        CinemachineShake.Instance.ShakeCamera(intensity, duration);
    }

    public static float GetActivationArea()
    {
        return activationArea;
    }

    public static Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }
} 
