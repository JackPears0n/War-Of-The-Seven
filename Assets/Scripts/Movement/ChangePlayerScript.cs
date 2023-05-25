using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ChangePlayerScript : MonoBehaviour
{
    States state;

    public GameObject tripObj;
    public GameObject krisObj;

    private PlayerHealthScript pHS;

    // Start is called before the first frame update
    void Start()
    {
        pHS = GetComponent<PlayerHealthScript>();

        pHS.isKrisActive = false;
        krisObj.SetActive(false);

        pHS.isTripActive = true;
        tripObj.SetActive(true);

        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeToTrip();
        }

        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeToKris();
        }
    }

    public void ChangeToTrip()
    {
        if (pHS.isKrisActive)
        {            
            // Activates the Trip object and makes it the current player
            pHS.isTripActive = true;
            tripObj.SetActive(true);

            // Deactivates Kris
            krisObj.SetActive(false);
            pHS.isKrisActive = false;

            // Makses the state idle
            state = States.Idle;
        }
        else
        {
            return;
        }

    }

    public void ChangeToKris()
    {
        if (pHS.isTripActive)
        {       
            // Activates the Kris object and makes it the current player
            pHS.isKrisActive = true;
            krisObj.SetActive(true);   

            // Deactivates Trip
            pHS.isTripActive = false;
            tripObj.SetActive(false);

            // Makses the state idle
            state = States.Idle;
        }
        else
        {
            return;
        }
    }

}
