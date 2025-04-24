using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Scripts")]
    public PauseMenu pauseMenu;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;

    [Header("PlayerController")]
    public PlayerController playerController;

    //variables not in inspector *very important*
    float timeElapsed;

    //ooo a gear icon so cool

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        UpdateTimer();
    }

    public void GMPause(bool input)
    {
        if(input)
        {
            pauseMenu.PauseGame();
        }
        else if (!input)
        {
            pauseMenu.ResumeGame();
        }
    }

    public void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        int milliseconds = Mathf.FloorToInt((timeElapsed % 1) * 1000);
        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    private void OnApplicationPause(bool pause) //uhh if you tab out thisll run
    {
        if (pause)
        {
            GMPause(true);
        }
        else //this runs if you tab back into the game, im not really sure if this will be used but ill leave it here
        {

        }
    }
}
