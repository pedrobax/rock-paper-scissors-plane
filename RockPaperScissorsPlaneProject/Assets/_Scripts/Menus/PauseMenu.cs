using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject HUD;
    public GameObject PauseMenuObject;
    public GameObject OptionsMenuObject;

    private void Update()
    {    
        if (OptionsMenuObject.activeSelf == true && Input.GetKeyDown(KeyCode.Escape)) GoToPauseMenu();
        else if (PauseMenuObject.activeSelf == true && Input.GetKeyDown(KeyCode.Escape)) ResumeGame();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        transform.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void GoToOptionsMenu()
    {
        OptionsMenuObject.SetActive(true);
        PauseMenuObject.SetActive(false);     
    }

    public void GoToPauseMenu()
    {
        PauseMenuObject.SetActive(true);
        OptionsMenuObject.SetActive(false);
    }

    public void ToggleHUD()
    {
        if (HUD.activeSelf) HUD.SetActive(false);
        else HUD.SetActive(true);
    }
}
