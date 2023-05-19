using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    States state;

    private PlayerHealthScript pHS;
    public GameObject pCS;

    [Header("Movement")]
    public float speed;
    public float groundDrag;

    // Jump variables
    public float jumpForce;
    private bool readyToJump;
    public float jumpCooldown = 1;
    public float airMultiplier = 1.1f;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask isGround;
    public bool grounded;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    public Transform orientation;



    float xInput;
    float yInput;

    Vector3 moveDirect;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        state = States.Idle;

        pHS = pCS.GetComponent<PlayerHealthScript>();

        // Gets the rigidbody and stops it rotating
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        grounded = true;
        readyToJump = true; 
    }

    // Update is called once per frame
    void Update()
    {
        // Orientation
        transform.rotation = orientation.rotation;

        // Movement logic
        DoLogic();
    }

    //-------------------
    // Enum Methods
    //-------------------

    private void DoLogic()
    {
        // Idle
        if (state == States.Idle)
        {
            Idle();
        }

        // Moving whilst falling / on the floor 
        if (state == States.Walking)
        {
            PlayerWalking();
        }

        // Jumping
        if (state == States.Jumping)
        {
            PlayerJumping();
        }

        // Movement while attacking
        if (state == States.Attacking)
        {
            PlayerAttacking();
        }
    }

    private void Idle()
    {
        // Makes player walk
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            state = States.Walking;
            PlayerWalking();
        }

        // Makes player jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            state = States.Jumping;
            PlayerJumping();
        }

    }

    private void PlayerWalking()
    {
        // Gets player input
        PlayerInput();

        // Casts a raycast to check if object is touching the ground
        // If its touching the ground, rest the jump
        GroundCheck();

        // Applies drag
        ApplyDrag();

        // Makes the player move
        MovePlayer();

        // Goes back to idle
        state = States.Idle;
    }

    private void PlayerJumping()
    { 
        
        // Casts a raycast to check if object is touching the ground
        // If its touching the ground, rest the jump
        GroundCheck();

        // Applies drag
        ApplyDrag();
        // Gets player input
        PlayerInput();

        // Checks if player can jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // Checks to see any horisontal input
        if (Input.GetButton("Horizontal"))
        {
            PlayerWalking();
        }
        else
        {
            // Goes back to idle
            state = States.Idle;
        }

    }

    private void PlayerAttacking()
    {
        // Ground Check
        GroundCheck();

        // Apply drag
        ApplyDrag();

        // Listens for input
        PlayerInput();

        // Moves the player
        MovePlayer();

        // Checks if player can jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // Stops the player going too fast
        SpeedControl();

    }

    //-------------------
    // Other Methods
    //-------------------
    private void PlayerInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        // Checks if player can jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // Calculate the movement direction
        moveDirect = orientation.forward * yInput + orientation.right * xInput;       

        //Checks if player is on the ground
        // Is grounded
        if (grounded)
        {
            rb.AddForce(moveDirect.normalized * speed * 10f, ForceMode.Force);
        }
        // Isn't grounded
        else if (!grounded)
        {
            rb.AddForce(moveDirect.normalized * speed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void ApplyDrag()
    {
        if (grounded)
        {
            rb.drag = groundDrag;
            rb.mass = 9.22f;
        }
        else
        {
            rb.drag = 0.5f;
            rb.mass = 30;
        }
    }

    private void SpeedControl()
    {
        // Defined the flat velocity variable
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Limits the velocity i it exceeds maximum
        if (flatVel.magnitude > speed)
        {
            Vector3 limitVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
    }

    private void GroundCheck()
    {
        grounded = Physics.Raycast(transform.position + (Vector3.up * 2),
            Vector3.down, playerHeight + 0.2f, isGround);
        Debug.DrawRay(transform.position + (Vector3.up * 2),
            Vector3.down, Color.red, (playerHeight + 0.2f));
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}