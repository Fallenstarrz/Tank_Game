using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // Master Mixer
    public AudioMixer masterMixer;

    // Set mixer volumes to saved volumes
    private void Start()
    {
        masterMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("musicVolume",0));
        masterMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("sfxVolume",0));
    }

    // function to set volume for exposed music volume parameter
    public void setMusicVolume(float volume)
    {
        masterMixer.SetFloat("MusicVolume", volume);
    }

    // function to set volume for exposed sfx volume parameter
    public void setSFXVolume(float volume)
    {
        masterMixer.SetFloat("SFXVolume", volume);
    }
}
