using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private GameObject levelShift;
    public string destination;

    // Update is called once per frame
    void Update()
    {
        if (levelShift != null)
        {
            SceneManager.LoadScene(destination);
        }
    }

    private void OnTriggerEnter(Collider colliosion)
    {
        if (colliosion.CompareTag("Player"))
        {
            levelShift = colliosion.gameObject;
        }
    }

}

