using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamScript : MonoBehaviour
{
    private PlayerHealthScript pHS;

    // Mouse sensativity
    public float xSens;
    public float ySens;

    public Transform camOrientation;
    public Transform tripOrientation;
    public Transform krisOrientation;

    public GameObject characterBody;
    public GameObject tripBody;
    public GameObject krisBody;

    // Camera rotation
    float xRotate;
    float yRotate;

    // Start is called before the first frame update
    void Start()
    {
        // Locks the cursor in place
        Cursor.lockState = CursorLockMode.Locked;

        // Makes cursor incisible
        Cursor.visible = false;

        pHS = GetComponent<PlayerHealthScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (pHS.isTripActive)
        {
            camOrientation.rotation = tripOrientation.rotation;
        }
        if (pHS.isKrisActive)
        {
            camOrientation.rotation = krisOrientation.rotation;
        }
        // Getting the mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySens;

        yRotate += mouseX;
        xRotate -= mouseY;


        // Clamps the axis view between
        xRotate = Mathf.Clamp(xRotate, -80, 80);



        // Roate camera and its orientation
        transform.rotation = Quaternion.Euler(xRotate, yRotate, 0);
        camOrientation.rotation = Quaternion.Euler(0, yRotate, 0);
    }
}
