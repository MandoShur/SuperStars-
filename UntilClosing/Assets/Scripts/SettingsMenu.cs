using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsMenuPanel;

    public Slider volume;


    void Start()
    {
        // Load saved settings (optional)
        volume.value = PlayerPrefs.GetFloat("Volume", 1f);
        

        ApplyVolume(volume.value);
    }

    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void ApplyVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    
}
