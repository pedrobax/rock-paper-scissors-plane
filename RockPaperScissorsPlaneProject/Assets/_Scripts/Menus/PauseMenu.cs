using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Functions for pause menu UI and pause functioning

    public GameObject HUD;
    public GameObject pauseMenuObject;
    public GameObject optionsMenuObject;
    public GameObject soundMenuObject;

    private void Update()
    {    
        //pauses game when escape is pressed or unpauses if already paused
        if (optionsMenuObject.activeSelf == true && Input.GetKeyDown(KeyCode.Escape)) GoToPauseMenu();
        else if (pauseMenuObject.activeSelf == true && Input.GetKeyDown(KeyCode.Escape)) ResumeGame();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        UnityEngine.Cursor.visible = false;
        transform.gameObject.SetActive(false);
        pauseMenuObject.SetActive(false);
        optionsMenuObject.SetActive(false);
        soundMenuObject.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Destroy(FindObjectOfType<GameManager>().gameObject);
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.Cursor.visible = true;
        Destroy(FindObjectOfType<GameManager>().gameObject);
        SceneManager.LoadScene(0);
    }

    public void GoToOptionsMenu()
    {      
        UnityEngine.Cursor.visible = true;
        soundMenuObject.SetActive(false);
        pauseMenuObject.SetActive(false);
        optionsMenuObject.SetActive(true);
    }

    public void GoToSoundMenu()
    {
        pauseMenuObject.SetActive(false);
        optionsMenuObject.SetActive(false);
        soundMenuObject.SetActive(true);
    }

    public void GoToPauseMenu()
    {
        soundMenuObject.SetActive(false);
        optionsMenuObject.SetActive(false);
        pauseMenuObject.SetActive(true);
    }

    public void ToggleHUD()
    {
        if (HUD.activeSelf) HUD.SetActive(false);
        else HUD.SetActive(true);
    }
}
