using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreNumber : MonoBehaviour
{
    // This class updates the score in the HUD and changes the color of the score text depending on the player type
    // for visual feedback

    public TMP_Text text;

    private void Start()
    {
        text.text = GameManager.overallScore.ToString(); // Set the score to the current score and write on HUD
        if (GameManager.currentPlayerType == GameManager.PlayerType.Rock) ChangeToRockColor();
        if (GameManager.currentPlayerType == GameManager.PlayerType.Paper) ChangeToPaperColor();
        if (GameManager.currentPlayerType == GameManager.PlayerType.Scissors) ChangeToScissorsColor();
    }

    void Update()
    {
        text.text = GameManager.overallScore.ToString();
        //Change the color of the score depending on the player type
        if (GameManager.currentPlayerType == GameManager.PlayerType.Rock) ChangeToRockColor();
        if (GameManager.currentPlayerType == GameManager.PlayerType.Paper) ChangeToPaperColor();
        if (GameManager.currentPlayerType == GameManager.PlayerType.Scissors) ChangeToScissorsColor();
    }

    void ChangeToPaperColor()
    {
        Color32 faceColor = new Color32(0, 255, 255, 255);
        Color32 outlineColor = new Color32(0, 12, 26, 255);
        Color32 gradientColor = new Color32(0, 62, 180, 255);

        text.colorGradient = new VertexGradient(Color.white, Color.white, gradientColor, gradientColor);
        text.faceColor = faceColor;
        text.outlineColor = outlineColor;
    }

    void ChangeToRockColor()
    {
        Color32 faceColor = new Color32(255, 0, 5, 255);
        Color32 outlineColor = new Color32(36, 0, 1, 255);
        Color32 gradientColor = new Color32(94, 0, 1, 255);

        text.colorGradient = new VertexGradient(Color.white, Color.white, gradientColor, gradientColor);
        text.faceColor = faceColor;
        text.outlineColor = outlineColor;
    }

    void ChangeToScissorsColor()
    {
        Color32 faceColor = new Color32(0, 255, 18, 255);
        Color32 outlineColor = new Color32(7, 29, 8, 255);
        Color32 gradientColor = new Color32(14, 79, 0, 255);

        text.colorGradient = new VertexGradient(Color.white, Color.white, gradientColor, gradientColor);
        text.faceColor = faceColor;
        text.outlineColor = outlineColor;
    }
}
