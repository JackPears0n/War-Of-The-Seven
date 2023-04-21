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
    public bool dead;

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
        if (state == States.Trip)
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


        if (state == States.Kris)
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
        if (state == States.Trip)
        {
            if (tripCurrentHealth <= 0)
            {
                dead = true;
                ChangePC("Kris");
            }
            else
            {
                dead = false;
                SaveHP();
            }
        }

        if (state == States.Kris)
        {
            if (tripCurrentHealth <= 0)
            {
                dead = true;
                ChangePC("Trip");
            }
            else
            {
                dead = false;
                SaveHP();
            }
        }

    }

    void LoadHP()
    {
        if (state == States.Trip)
        {
            tripCurrentHealth = PlayerPrefs.GetInt("TripHP");
            sliderValueHP.value = tripCurrentHealth;
            UpdateHPSlider(tripCurrentHealth);
        }

        if (state == States.Kris)
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
        if (state == States.Trip)
        {
            tripCurrentHealth -= damage;
        }

        if (state == States.Kris)
        {
            krisCurrentHealth -= damage;
        }

    }

    public void Heal(int heal)
    {
        if (state == States.Trip)
        {
            tripCurrentHealth += heal;
        }

        if (state == States.Kris)
        {
            krisCurrentHealth += heal;
        }
        
    }

    void ChangePC(string pCName)
    {
        if (state == States.Trip)
        {
            tripHolder.SetActive(false);
            krisHolder.SetActive(true);
        }
    }
}
