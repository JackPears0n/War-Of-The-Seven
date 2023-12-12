using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{

    public string[] characters = { "Trip", "Kris" };
    public MonoBehaviour[] health;
    public MonoBehaviour[] combat;
    public string activeCharacter;

    States state;

    public GameObject tripObj;
    public GameObject krisObj;

    private TripHealth tHS;
    private KrisHealth kHS;

    // Start is called before the first frame update
    void Start()
    {
        tHS = tripObj.GetComponent<TripHealth>();
        tHS.istripActive = true;
        tripObj.SetActive(true);

        kHS = krisObj.GetComponent<KrisHealth>();
        kHS.isKrisActive = false;
        krisObj.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePlayer();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangePlayer();
        }
    }

    public void ChangePlayer()
    {
        #region dead player
        if (tHS.istripDead && kHS.isKrisDead)
        {
            Die();
        }
        else if (tHS.istripDead)
        {
            activeCharacter = characters[1];
        }
        else if (kHS.isKrisDead)
        {
            activeCharacter = characters[0];
        }
        #endregion
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            state = States.Dead;
            Die();
        }
    }

    void Die()
    {
        if (state == States.Dead)
        {
            SceneManager.LoadScene("Game Over");
        }
    }

}
