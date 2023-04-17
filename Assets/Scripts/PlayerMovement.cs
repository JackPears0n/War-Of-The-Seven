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
        // Gets the rigidbody and stops it rotating
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        grounded = true;
        readyToJump = true; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = orientation.rotation;



        // Ground Check
        //grounded = DoRayCollisionCheck();
        grounded = Physics.Raycast(transform.position + (Vector3.up * 2), Vector3.down, playerHeight + 0.2f, isGround);
        Debug.DrawRay(transform.position + (Vector3.up * 2), Vector3.down, Color.red, (playerHeight + 0.2f));


        // Listens for input
        PlayerInput();    

        // Apply drag
        if (grounded)
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
        if(grounded)
        {
            rb.AddForce(moveDirect.normalized * speed * 10f, ForceMode.Force);
        }
        // Isn't grounded
        else if(!grounded)
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

    // Ground check method
    /*
    public bool DoRayCollisionCheck()
    {
        // Cast a ray downward which is slightly longer than the player
        RaycastHit hit = Physics.Raycast(transform.position, -Vector3.up, playerHeight + 0.2f, isGround);

        hit = Physics.Raycast(transform.position, -Vector3.up, playerHeight, isGround);

        // Show ray in editor
        Debug.DrawRay(transform.position, -Vector3.up * (playerHeight + 0.2f), (hit.collider != null) ? Color.black : Color.red);

        if (hit.collider.tag == "Ground")
        {
            return grounded == true;
        }
        else
        {
            return grounded;
        }    
    }
    */
}