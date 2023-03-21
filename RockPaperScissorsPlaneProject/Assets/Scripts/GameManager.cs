using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event Action<float> ScoreUpdated;
    public LevelArea _levelArea;
    public Transform _playerTransform;
    static float activationArea;
    static Vector3 playerPosition;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        activationArea = _levelArea.verticalAreaLimit * 2;
        Debug.Log("Activation Area: " + activationArea);
    }

    private void OnDrawGizmos()
    {
        playerPosition = _playerTransform.position;
    }

    private void Update()
    {
        playerPosition = _playerTransform.position;
    }

    public static void UpdateScore(float score)
    {
        ScoreUpdated?.Invoke(score);
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
