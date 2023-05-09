using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHUDType : MonoBehaviour
{
    //TODO this class is unused, delete it after checking if its ok

    public GameObject HPHUDRock;
    public GameObject HPHUDPaper;
    public GameObject HPHUDScissors;

    private void Start()
    {
        if (GameManager.currentPlayerType == GameManager.PlayerType.Rock)
        {
            HPHUDRock.SetActive(true);
            HPHUDPaper.SetActive(false);
            HPHUDScissors.SetActive(false);
        }
        if (GameManager.currentPlayerType == GameManager.PlayerType.Paper)
        {
            HPHUDRock.SetActive(false);
            HPHUDPaper.SetActive(true);
            HPHUDScissors.SetActive(false);
        }
        if (GameManager.currentPlayerType == GameManager.PlayerType.Scissors)
        {
            HPHUDRock.SetActive(false);
            HPHUDPaper.SetActive(false);
            HPHUDScissors.SetActive(true);
        }
    }

    private void OnEnable()
    {
        if (GameManager.currentPlayerType == GameManager.PlayerType.Rock)
        {
            HPHUDRock.SetActive(true);
            HPHUDPaper.SetActive(false);
            HPHUDScissors.SetActive(false);
        }
        if (GameManager.currentPlayerType == GameManager.PlayerType.Paper)
        {
            HPHUDRock.SetActive(false);
            HPHUDPaper.SetActive(true);
            HPHUDScissors.SetActive(false);
        }
        if (GameManager.currentPlayerType == GameManager.PlayerType.Scissors)
        {
            HPHUDRock.SetActive(false);
            HPHUDPaper.SetActive(false);
            HPHUDScissors.SetActive(true);
        }
    }

    private void Update()
    {
        if (GameManager.currentPlayerType == GameManager.PlayerType.Rock)
        {
            HPHUDRock.SetActive(true);
            HPHUDPaper.SetActive(false);
            HPHUDScissors.SetActive(false);
        }
        if (GameManager.currentPlayerType == GameManager.PlayerType.Paper)
        {
            HPHUDRock.SetActive(false);
            HPHUDPaper.SetActive(true);
            HPHUDScissors.SetActive(false);
        }
        if (GameManager.currentPlayerType == GameManager.PlayerType.Scissors)
        {
            HPHUDRock.SetActive(false);
            HPHUDPaper.SetActive(false);
            HPHUDScissors.SetActive(true);
        }
    }
}
