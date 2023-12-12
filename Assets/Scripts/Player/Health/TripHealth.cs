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

public class TripHealth : MonoBehaviour
{
    States state;
    private ChangePlayerScript changePlayerScript;

    [Header("Character holders")]
    public GameObject tripHolder;

    [Header("Bools")]
    public bool istripActive;
    public bool istripDead;

    // Health values
    [Header("Health values")]
    public int tripCurrentHealth;
    public int tripCurrentMaxHealth;
    public int tripMaxHealth;

    // Invinsability values
    [Header("Invinsability values")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    public bool invulnerable;

    // Health Shield
    [Header("Health Shield")]
    public int shieldStrength;
    public int currentShield;

    // HP slider
    [Header("Slider")]
    [SerializeField] Slider sliderValueHP = null;
    [SerializeField] Slider sliderValueShield = null;

    void Start()
    {
        /*
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
        
        
        if (istripActive)
        {
            if (!PlayerPrefs.HasKey("tripHP"))
            {
                PlayerPrefs.SetInt("tripHP", 100);
                LoadHP();
            }
            else
            {
                LoadHP();
            }
        }*/

        tripCurrentMaxHealth = tripMaxHealth;

        tripCurrentHealth = tripMaxHealth;
    }

    void Update()
    {
        CheckHealth();
        UpdateSheildSlider(currentShield);
    }

    void CheckHealth()
    {
        if (istripActive)
        {
            if (tripCurrentHealth <= 0)
            {
                istripDead = true;
                changePlayerScript.ChangeToTrip();
            }
            else
            {
                istripDead = false;
                UpdateHPSlider(tripCurrentHealth);
            }
        }

        if (tripCurrentHealth > tripCurrentMaxHealth)
        {
            tripCurrentHealth = tripCurrentMaxHealth;
        }

    }

    void LoadHP()
    {

        if (istripActive)
        {
            tripCurrentHealth = PlayerPrefs.GetInt("tripHP");
            sliderValueHP.value = tripCurrentHealth;
            UpdateHPSlider(tripCurrentHealth);
        }

    }

    public void SaveHP()
    {
        PlayerPrefs.SetInt("tripHP", tripCurrentHealth);
    }

    void UpdateHPSlider(int currentHealth)
    {

        if (istripActive)
        {
            sliderValueHP.maxValue = tripCurrentMaxHealth;
            sliderValueHP.value = currentHealth;
            //SaveHP();
        }
    }

    void UpdateSheildSlider(int ss)
    {
        sliderValueShield.maxValue = tripMaxHealth;
        sliderValueShield.value = ss;
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
        if (!invulnerable)
        {
            if (istripActive)
            {
                if (currentShield > 0)
                {
                    currentShield -= damage;
                }
                else
                {
                    tripCurrentHealth -= damage;
                }

            }
        }
    }

    public void Heal(int heal)
    {
        if (istripActive)
        {
            tripCurrentHealth += heal;
        }

    }

    public void MakeAShield(int ss)
    {

        shieldStrength = ss;
        currentShield += shieldStrength;
        if (currentShield > tripMaxHealth)
        {
            currentShield = tripMaxHealth;
        }
    }
    public void Invunerability(float duration)
    {
        invulnerable = true;
        Invoke(nameof(EndInvunerability), duration);

    }

    public void EndInvunerability()
    {
        print("Invunerability is off");
        invulnerable = false;
    }
}
