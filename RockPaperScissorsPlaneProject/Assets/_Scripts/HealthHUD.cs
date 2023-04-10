using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    public Image[] hearts;

    private void Start()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < GameManager.currentPlayerHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < GameManager.currentPlayerHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < GameManager.currentPlayerHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
