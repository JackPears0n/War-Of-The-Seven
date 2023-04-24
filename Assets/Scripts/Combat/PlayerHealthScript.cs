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

public class PlayerHealthScript : MonoBehaviour
{
    //-----------
    // Variables
    //-----------

    States state; 

    [Header("Character holders")]
    public GameObject tripHolder;
    public GameObject krisHolder;

    [Header("Bools")]
    public bool isTripActive = true;
    public bool isKrisActive;
    public bool isTripDead;
    public bool isKrisDead;

    // Health values
    [Header("Health values")]
    public int tripCurrentHealth;
    public int krisCurrentHealth;
    public int tripMaxHealth;
    public int krisMaxHealth;

    // HP slider
    [Header("Slider")]
    [SerializeField] Slider sliderValueHP = null;

    void Start()
    {
        if (isTripActive)
        {
            if (!PlayerPrefs.HasKey("TripHP"))
            {
                PlayerPrefs.SetInt("TripHP", 50);
                LoadHP();
            }
            else
            {
                LoadHP();
            }
        }


        if (isKrisActive)
        {
            if (!PlayerPrefs.HasKey("KrisHP"))
            {
                PlayerPrefs.SetInt("KrisHP", 100);
                LoadHP();
            }
            else
            {
                LoadHP();
            }
        }

    }

    void Update()
    {
        CheckHealth();
    }

    void CheckHealth()
    {
        if (isTripActive)
        {
            if (tripCurrentHealth <= 0)
            {
                isTripDead = true;
                ChangePC("Kris");
            }
            else
            {
                isTripDead = false;
                SaveHP();
            }
        }

        if (isKrisActive)
        {
            if (krisCurrentHealth <= 0)
            {
                isKrisDead = true;
                ChangePC("Trip");
            }
            else
            {
                isKrisDead = false;
                SaveHP();
            }
        }
        
        /*
        if (isTripDead && isKrisDead)
        {
            state = States.Dead;
            Die();
        }
        */
    }

    void LoadHP()
    {
        if (isTripActive)
        {
            tripCurrentHealth = PlayerPrefs.GetInt("TripHP");
            sliderValueHP.value = tripCurrentHealth;
            UpdateHPSlider(tripCurrentHealth);
        }

        if (isKrisActive)
        {
            krisCurrentHealth = PlayerPrefs.GetInt("KrisHP");
            sliderValueHP.value = krisCurrentHealth;
            UpdateHPSlider(krisCurrentHealth);
        }

    }

    public void SaveHP()
    {
        PlayerPrefs.SetInt("TripHP", tripCurrentHealth);
        PlayerPrefs.SetInt("KrisHP", krisCurrentHealth);
    }

    void UpdateHPSlider(int currentHealth)
    {
        sliderValueHP.value = currentHealth;
        SaveHP();
    }

    void Die()
    {
        if (state == States.Dead)
        {
          SceneManager.LoadScene("Game Over");
        }
    }

    public void LoseHealth(int damage)
    {
        if (isTripActive)
        {
            tripCurrentHealth -= damage;
        }

        if (isKrisActive)
        {
            krisCurrentHealth -= damage;
        }

    }

    public void Heal(int heal)
    {
        if (isTripActive)
        {
            tripCurrentHealth += heal;
        }

        if (isKrisActive)
        {
            krisCurrentHealth += heal;
        }
        
    }

    public void ChangePC(string pCName)
    {
        if (isTripActive && isKrisDead == false)
        {
            tripHolder.SetActive(false);
            krisHolder.SetActive(true);
            isTripActive = false;
            isKrisActive = true;
        }

        if (isKrisActive && isTripDead == false)
        {
            krisHolder.SetActive(false);
            tripHolder.SetActive(true);
            isKrisActive = false;
            isTripActive = true;
        }

        if (isTripDead && isKrisDead)
        {
            state = States.Dead;
        }
    }
}
