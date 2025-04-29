using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject blackBG;
    //private bool isPaused = false;

    void Update()
    {
        
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        blackBG.SetActive(false);
        Time.timeScale = 1f; // resume time
        //isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        blackBG.SetActive(true);
        Time.timeScale = 0f; // pause time
        //isPaused = true;
    }

}
