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

    // Start is called before the first frame update
    void Start()
    {
        krisHolder.SetActive(false);
        tripHolder.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            krisHolder.SetActive(false);
            tripHolder.transform.position = krisHolder.transform.position;
            tripHolder.SetActive(true);
            state = States.Idle;

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            tripHolder.SetActive(false);
            krisHolder.transform.position = tripHolder.transform.position;
            krisHolder.SetActive(true);
            state = States.Idle;

        }
    }
}
