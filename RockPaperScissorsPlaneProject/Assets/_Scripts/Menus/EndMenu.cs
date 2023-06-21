using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    //functions for the buttons on the End Menu UI

    private void Start() {
        UnityEngine.Cursor.visible = true;
    }
    public void RestartGame()
    {
        UnityEngine.Cursor.visible = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        UnityEngine.Cursor.visible = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
