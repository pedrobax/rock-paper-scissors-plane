using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverrideMusic : MonoBehaviour
{
    public AudioClip audioClip;
    void Start()
    {
        GameManager.ChangeMusic(audioClip);
    }
}
