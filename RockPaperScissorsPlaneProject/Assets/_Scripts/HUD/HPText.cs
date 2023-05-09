using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HPText : MonoBehaviour
{
    //TODO this class isn't used anymore, check if its ok and delete it

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
