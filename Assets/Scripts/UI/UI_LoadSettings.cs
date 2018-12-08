using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LoadSettings : MonoBehaviour
{
    // UI variables
    public Slider musicVolume;
    public Slider sfxVolume;
    public Toggle randomMap;
    public Toggle mapOfDay;
    public Toggle seededMap;
    public Toggle isMultiEnabled;
    public InputField seedNumber;
    public int number;
    public int mapType;
    public Dropdown p1Controller;
    public Dropdown p2Controller;

    public void loadSettings()
    {
        // SFX Volume
        sfxVolume.value = PlayerPrefs.GetFloat("sfxVolume", 0);
        GameManager.instance.sfxVol = PlayerPrefs.GetFloat("sfxVolume", 0);

        // Music Volume
        musicVolume.value = PlayerPrefs.GetFloat("musicVolume", 0);
        GameManager.instance.musicVol = PlayerPrefs.GetFloat("musicVolume", 0);

        // Multiplayer
        if (PlayerPrefs.GetInt("isMultiplayer", 0) == 0)
        {
            isMultiEnabled.isOn = false;
            GameManager.instance.isMultiplayer = false;
        }
        else
        {
            isMultiEnabled.isOn = true;
            GameManager.instance.isMultiplayer = true;
        }

        // Seed Number Settings
        number = PlayerPrefs.GetInt("seedNumber", number);
        GameManager.instance.seedNum = number;
        seedNumber.text = number.ToString();

        // Map Type
        mapType = PlayerPrefs.GetInt("mapMode", 0);
        if (mapType == 0)
        {
            mapOfDay.isOn = true;
            randomMap.isOn = false;
            seededMap.isOn = false;
        }
        else if (mapType == 1)
        {
            mapOfDay.isOn = false;
            randomMap.isOn = true;
            seededMap.isOn = false;
        }
        else
        {
            mapOfDay.isOn = false;
            randomMap.isOn = false;
            seededMap.isOn = true;
        }
        GameManager.instance.mapMode = PlayerPrefs.GetInt("mapMode", 0);

        // Save PlayerController 1
        GameManager.instance.player1Controller = PlayerPrefs.GetInt("Player1Controller", 0);
        p1Controller.value = PlayerPrefs.GetInt("Player1Controller", 0);

        // Save PlayerController 2
        GameManager.instance.player2Controller = PlayerPrefs.GetInt("Player2Controller", 1);
        p2Controller.value = PlayerPrefs.GetInt("Player2Controller", 1);
    }
}
