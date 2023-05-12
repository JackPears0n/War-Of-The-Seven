using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamScript : MonoBehaviour
{
    public Transform camPos;
   
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        transform.position = camPos.position;
    }

}
