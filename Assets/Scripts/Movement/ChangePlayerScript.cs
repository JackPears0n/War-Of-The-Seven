using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ChangePlayerScript : MonoBehaviour
{
    States state;

    [Header("Character holders")]
    public GameObject tripHolder;
    public GameObject krisHolder;

    private PlayerHealthScript pHS;

    // Start is called before the first frame update
    void Start()
    {
        pHS = GetComponent<PlayerHealthScript>();

        pHS.isKrisActive = false;
        krisHolder.SetActive(false);

        pHS.isTripActive = true;
        tripHolder.SetActive(true);

        
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

    void ChangeToTrip()
    {
        if (pHS.isKrisActive)
        {            
            // Activates the Trip object and makes it the current player
            pHS.isTripActive = true;
            tripHolder.SetActive(true);

            // Moves Trip to where Kris was
            tripHolder.transform.position = krisHolder.transform.position;

            // Deactivates Kris
            krisHolder.SetActive(false);
            pHS.isKrisActive = false;

            // Makses the state idle
            state = States.Idle;
        }
        else
        {
            return;
        }

    }

    void ChangeToKris()
    {
        if (pHS.isTripActive)
        {       
            // Activates the Kris object and makes it the current player
            pHS.isKrisActive = true;
            krisHolder.SetActive(true);   
            
            // Moves Kris to where Trip was
            krisHolder.transform.position = tripHolder.transform.position;

            // Deactivates Trip
            pHS.isTripActive = false;
            tripHolder.SetActive(false);

            // Makses the state idle
            state = States.Idle;
        }
        else
        {
            return;
        }
    }

}
