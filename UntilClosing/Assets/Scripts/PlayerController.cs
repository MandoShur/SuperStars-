using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //unaccessables and things youre not supposed to edit at all DO NOT EDIT THESE OR YOU EXPLODE VIOLENTLY(and code messes up but thats not important)
    [SerializeField] private bool _grounded; 
    private float baseGravityScale;
    [SerializeField] private bool _isPaused = false;

    [Header("Key Binds")] //maybe in settings this'll be changable(eventually)?
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode diveKey = KeyCode.E;
    public KeyCode pauseKey = KeyCode.Escape;


    [Header ("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float airMovementMulti;

    [Header ("Gravity Stuff")]
    public float gravityScale = 8.5f;
    public float VerticalVelocityThreshold = 1.4f;
    public float AccelerationRate = 0.2f;
    private float TerminalVelo = -14f; //this should be in unaccessables but idc!!!



    [Header ("Jumping")]
    public float jumpForce;
    public float jumpCD;
    [SerializeField] bool readyToJump = true;
    bool readyToDoubleJump = true;

    [Header("Diving")]
    public float diveForce;
    bool readyToDive = true;
    bool isInDiveState = true;

    [Header ("Grounded Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded
    {
        get { return _grounded; } 
        set //basically just resets double jump and dive cd when youre grounded
        {
            //Debug.Log("set run, NOT the if statement");
            if(_grounded == false && value == true) //if grounded value changes to true
            {
                //Debug.Log("set IF statement ran");
                //Debug.Log("grounded set to true");
                AirborneCooldownResets();
            }
            _grounded = value; //set "_grounded" value to "grounded"
        }
    }

    [Header ("Assigned Objects")]
    public Transform orientation;
    public Transform playerObject; //currently just a fallback, rarely used

    //Other bools

    private float horizontalInput;
    private float verticalInput;

    Vector3 moveDir;

    public Rigidbody rb; //this is just public so debugging script can catch it

    bool isPaused
    {
        get { return _isPaused; }
        set
        {
            if(isPaused == true)
            {
                Time.timeScale = 0f;
                _isPaused = isPaused;
            }
            else if(isPaused == true)
            {
                Time.timeScale = 1f;
                _isPaused = isPaused;
            }
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false; //disables gravity, using custom gravity

        baseGravityScale = gravityScale; //establishing base gravity
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, -transform.up, playerHeight * 0.5f + 0.3f, whatIsGround); //ground check

        MyInput();
        //SpeedControl();

        if (grounded) //dynamic drag system, though it isnt really doing anything atm since both are set to grounddrag (redundant)
        {
            rb.drag = groundDrag;
            readyToDive = true;
        } 
        else //airborne drag
        {
            rb.drag = groundDrag; //testing currently
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();

        GravityForce();
    }

    private void GravityForce() //essentially just gravity handling
    {
        if (rb.velocity.y > VerticalVelocityThreshold && !grounded)
        {
            rb.AddForce(Physics.gravity, ForceMode.Acceleration); //regular gravity on ground
        }
        else //note to self terminal velo AT 8 gravscale is -14.12639 
        {
            rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration); //if y velocity falls below float, increase gravity to make jump less floaty
            if(rb.velocity.y < TerminalVelo) //if, in increased gravity, terminal velocity is reached (i think -14) begin to increase gravity overtime 
            {
                Debug.Log("coroutine started");
                StartCoroutine(FallFaster()); //small(kinda redundant) note, the camera gets really zoomed out if you fall for longer than like 2.5 seconds, but if youre falling for that long you probably fell off the map 
            }
        }
        if(rb.velocity.y >= -0.1f) //if gravity is above baseline minus a bit (not falling) reset gravity to original gravity
        {
            gravityScale = baseGravityScale;
        }
    }

    IEnumerator FallFaster() //wait for 1/10th of a second then add to gravity scale with accel rate every call
    {
        yield return new WaitForSeconds(0.1f);
        gravityScale = AccelerationRate + gravityScale;
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(pauseKey))
        {
            isPaused = !isPaused;
        }

        if (Input.GetKey(jumpKey) && readyToJump && grounded && !isPaused) //grounded jump check 
        {
            Debug.Log("jumpCalled");
            readyToJump = false;
            Jump(); //jump (wow)

            Invoke(nameof(ResetJump), jumpCD); //jumpCD
        }
        else if (Input.GetKeyDown(jumpKey) && readyToDoubleJump && readyToJump && !isPaused) //definitely a better way i couldve scripted this
        {
            Debug.Log("double jumped");
            readyToDoubleJump = false;
            Jump(); //jump (wow, again!)

            Invoke(nameof(ResetJump), jumpCD);
        }

        if (Input.GetKey(diveKey) && readyToDive && !grounded && !isPaused)
        {
            readyToDive = false;

            Dive();
        }
    }

    void MovePlayer()
    {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput; //at one point i had vertical and horizontal inputs swapped lol

        if(grounded)
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Acceleration);

        else if(!grounded)
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMovementMulti, ForceMode.Force); //this is probably gonna become redundant (it has)
    }

    /*private void SpeedControl() //this is most likely going to be heavily changed later. or be redundant.
    {
        Vector3 flatVel = new Vector3 (rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVelo = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3 (limitedVelo.x, rb.velocity.y, limitedVelo.z);
        }
    }*/

    private void Jump() //wow its a jump
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //cancel any vertical velocity

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); //jump force
    }

    private void ResetJump() //after whatever timer/method used to invoke, jump cd off
    {
        readyToJump = true; //note to self dont set bools to "!bool" for stuff anymore or itll bug
    }

    private void Dive()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //cancel any vertical velocity

        Debug.Log("Dive called");

        Vector3 moveDirec = moveDir;
        if(moveDirec == Vector3.zero)
        {
            Debug.LogWarning("moveDir at 0, fallback to playerObj");
            moveDirec = playerObject.forward;
        }
        rb.AddForce((moveDirec + (transform.up * 0.6f)).normalized * diveForce, ForceMode.Impulse); //
    }

    public void AirborneCooldownResets() //generic reset for dive and djump. this will probably be used in a few places so im making a reset for this here.
    {
        readyToDoubleJump = true;
        readyToDive = true;
    }



    //comment here because im going to forget if i dont, possibly split jump into 2 parts, rising and falling (y.velocity > 0, y.velocity < 0)
    //could give more control over jump curve, and especially over the dumb thing where i either descend super slowly or jump super fast and abruptly stop
    //only problem i see with this is exception cases (which i can manually work with) and just keeping track of it
    //update to above 4/11, implemented this
}
