using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public GameObject playerObj;
    public Transform orient;
    float xRotate;
    float yRotate;
    float zRotate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xRotate = orient.rotation.x;
        yRotate = orient.rotation.y;
        zRotate = orient.rotation.z;
        gameObject.transform.rotation = Quaternion.Euler(0f, xRotate, 0f);
    }
}
