using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode diveKey = KeyCode.E;


    [Header ("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float airMovementMulti;

    [Header ("Jumping")]
    public float jumpForce;
    public float jumpCD;
    bool readyToJump = true;

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
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCD);
        }
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
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = !readyToJump;
    }
}
