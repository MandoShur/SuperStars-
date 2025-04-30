using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject blackBG;
    //private bool isPaused = false;

    public void ResumeGame() //resumes the game
    {
        pauseMenuUI.SetActive(false);
        blackBG.SetActive(false);
        Time.timeScale = 1f; // resume time
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //isPaused = false;
    }

    public void PauseGame() //pauses the game, shocking
    {
        pauseMenuUI.SetActive(true);
        blackBG.SetActive(true);
        Time.timeScale = 0f; // pause time
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //isPaused = true;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
