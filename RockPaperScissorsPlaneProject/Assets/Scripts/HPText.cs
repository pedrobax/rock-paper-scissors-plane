using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HPText : MonoBehaviour
{
    public Text hpTextNumber;
    public PlayerHealth playerHealth;

    private void Start()
    {
        hpTextNumber.text = playerHealth.lives.ToString();
    }

    private void Update()
    {
        hpTextNumber.text = playerHealth.lives.ToString();
    }
}
