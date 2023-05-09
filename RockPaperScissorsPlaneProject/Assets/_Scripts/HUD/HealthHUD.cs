using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    //This class is used to display the player's health state on the HUD
    //The max amount of hearts that can be displayed is determined by the array size in the inspector

    public Image[] hearts; //array of "heart" images to display

    //Start, OnEnable and Update are used to update the display of the hearts based on the player's health
    //these three are needed to prevent visual bugs when starting the level of closing menus
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
