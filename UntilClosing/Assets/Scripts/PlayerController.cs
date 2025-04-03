using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;
    //dont use this yet     public KeyCode diveKey = KeyCode.E;


    [Header ("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float airMovementMulti;

    [Header ("Jumping")]
    public float jumpForce;
    public float jumpCD;
    bool readyToJump = true;
    bool readyToDoubleJump = true;

    [Header("Diving")]
    public float diveForce;
    bool readyToDive = true;

    [Header ("Grounded Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header ("Assigned Objects")]
    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    Vector3 moveDir;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false; //disables gravity
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
            readyToDive = true;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();

        //custom gravity code, hopefully will fix jumps feeling really floaty on descent 
        rb.AddForce(Physics.gravity * 2f, ForceMode.Acceleration); //ok i think it worked
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded) //grounded jump check 
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCD);
        }

        /*if(Input.GetKey(diveKey) && readyToDive && !grounded)
        {
            readyToDive = false;

            Dive();

        } whoops not implemented yet :p */
    }

    void MovePlayer()
    {
        moveDir = orientation.forward * horizontalInput + orientation.right * verticalInput;

        if(grounded)
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);

        else if(!grounded)
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMovementMulti, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3 (rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVelo = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3 (limitedVelo.x, rb.velocity.y, limitedVelo.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //cancel any vertical velocity

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = !readyToJump;
    }

    private void Dive()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //cancel any vertical velocity

        rb.AddForce((transform.right + transform.up)/2 * diveForce, ForceMode.Impulse); //i think the axis i put should work for a dive direction but im not sure and i cant check while im writing this. 
    }
}
