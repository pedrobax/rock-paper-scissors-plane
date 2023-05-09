using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    //Volume slider functions

    public VolumeGroup volumeGroup;
    public AudioMixer mixer;
    public Slider volumeSlider;

    private void Start()
    {
        if (volumeGroup == VolumeGroup.Master) volumeSlider.value = PlayerPrefs.GetFloat("MasterVol", 0.75f);
        else if (volumeGroup == VolumeGroup.Music) volumeSlider.value = PlayerPrefs.GetFloat("MusicVol", 0.75f);
        else if (volumeGroup == VolumeGroup.SFX) volumeSlider.value = PlayerPrefs.GetFloat("SFXVol", 0.75f);
    }

    //changes volume and stores it on player prefs so it stays the same when the game is closed and reopened
    public void SetMasterLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVol", sliderValue);
    }

    //changes volume and stores it on player prefs so it stays the same when the game is closed and reopened
    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVol", sliderValue);
    }

    //changes volume and stores it on player prefs so it stays the same when the game is closed and reopened
    public void SetSFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVol", sliderValue);
    }

    public enum VolumeGroup { Master, Music, SFX}
}
