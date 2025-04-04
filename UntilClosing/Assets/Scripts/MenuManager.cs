using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel; 
    public GameObject playMenuPanel; 

    // Show the Play Menu and hide the Main Menu
    public void OpenPlayMenu()
    {
        mainMenuPanel.SetActive(false);
        playMenuPanel.SetActive(true);
    }

    // Go back to the Main Menu
    public void BackToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        playMenuPanel.SetActive(false);
    }
}
