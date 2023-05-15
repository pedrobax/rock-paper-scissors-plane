using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialPopup : MonoBehaviour
{
    //this object will be set active by the Encounter class and display the text on the screen

    public TMP_Text popupText;

    public void ResumeGame()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
}
