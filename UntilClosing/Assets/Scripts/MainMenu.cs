using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        
    }

   
    public void OpenOptions() //redundant
    {
        UnityEngine.Debug.Log("Options Menu Opened");
    }

    // Exits the game
    public void QuitGame()
    {
        UnityEngine.Debug.Log("Game Quit");
        UnityEngine.Application.Quit();
    }
}
