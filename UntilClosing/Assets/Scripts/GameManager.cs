using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour     //ooo a gear icon so cool
{
    [Header("UI Scripts")]
    public PauseMenu pauseMenu;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public GameObject goalUIScreen;
    public TextMeshProUGUI goalTimer;

    [Header("PlayerController")]
    public PlayerController playerController;

    [Header("Goal Object")]
    public GameObject goal;

    [Header("Checkpoint Objects")]
    public GameObject[] checkPObjects;

    [Header("hidden variables")] //this should not show in inspector unless debugging
    private int currentCheckpoint; //should start at 0, aka start of level
    float timeElapsed;
    private float finishTime; //holder float value for timer display at goal end

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

    public void OnGoalSuccess()
    {
        goalUIScreen.SetActive(true);
        finishTime = timeElapsed;
        
        int minutes = Mathf.FloorToInt(finishTime / 60);
        int seconds = Mathf.FloorToInt(finishTime % 60);
        int milliseconds = Mathf.FloorToInt((finishTime % 1) * 1000);
        goalTimer.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds); //theres a better way i couldve done this but idc
    }

    public void OnDeath()
    {
        //Death VFX here
        Invoke(nameof(PlayerCheckpointReset), 2f);
    }

    public void PlayerCheckpointReset()
    {
        playerController.gameObject.transform.position = Vector3.up;
        playerController.gameObject.GetComponentInChildren<CapsuleCollider>().gameObject.transform.rotation = Quaternion.identity;
        playerController.isDead = false;
        Debug.Log("after teleport, isdead = " + playerController.isDead);
    }
}
