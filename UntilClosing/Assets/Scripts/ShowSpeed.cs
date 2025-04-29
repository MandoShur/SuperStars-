using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowSpeed : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject player;
    public Rigidbody playerRb;
    private bool isGravDownwards;
    public PlayerController playerController;
    public float lowestYVelo = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerRb.velocity.y < playerController.VerticalVelocityThreshold)
        {
            isGravDownwards = true;
        }
        if (playerRb.velocity.y >= -0.1f)
        {
            isGravDownwards = false;
        }
        text.text = "Downwards Grav: " + isGravDownwards;


        if(playerRb.velocity.y < lowestYVelo)
        {
            lowestYVelo = playerRb.velocity.y;
        }
    }

    private void Update()
    {
        //Debug.Log("isDead = " + playerController.isDead);
    }
}
//this is more of a debugging script than anything, not gonna comment on this since its just whatever