using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float groundDrag;
    // Jump variables
    public float jumpForce;
    private bool readyToJump;
    public float jumpCooldown = 1;
    public float airMultiplier = 2;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask isground;
    bool isItGrounded;

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
        // Gets the rigidbody and stops it rotating
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = camera.rotation;

        // Listens for input
        PlayerInput();

        // Ground Check
        isItGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isground);

        // Apply drag
        if (isItGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        // Stops the player going too fast
        SpeedControl();

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        // Checks if player can jump
        if (Input.GetKey(jumpKey) && readyToJump && isItGrounded)
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
        if(isItGrounded)
        {
            rb.AddForce(moveDirect.normalized * speed * 10f, ForceMode.Force);
        }
        // Isn't grounded
        else if(!isItGrounded)
        {
            rb.AddForce(moveDirect.normalized * speed * 10f * airMultiplier, ForceMode.Force);
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
