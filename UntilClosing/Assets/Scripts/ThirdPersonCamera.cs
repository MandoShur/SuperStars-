using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* script for turning playerObject towards input direction relative to camera 
 * so if i press "d" object will turn to face right from the camera, and if i move camera and do it again itll face right from camera view again
 * why this script was made to go on the camera itself and not the player idrk, but im just following a tutorial (im not just copying it i swear)*/

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb; //this just straight up hasnt been used yet? not sure why its even here

    public float rotationSpeed;

    private void Start() //make cursor locked in center and invisible blah blah blah
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //calculates a vector from camera position to player position on x and z 
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized; //rotates orientation to above vector

        //get player inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput; //inputs into a vector using orientation

        if(inputDir != Vector3.zero) //if input
        {
            //slerps playerObj to face towards the input direction in above line by rotSpeed * time.delta
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
