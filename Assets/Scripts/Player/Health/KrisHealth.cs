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

public class KrisHealth : MonoBehaviour
{
    //-----------
    // Variables
    //-----------

    States state;
    private ChangePlayerScript changePlayerScript;

    [Header("Character holders")]
    public GameObject krisHolder;

    [Header("Bools")]
    public bool isKrisActive;
    public bool isKrisDead;

    // Health values
    [Header("Health values")]
    public int krisCurrentHealth;
    public int krisCurrentMaxHealth;
    public int krisMaxHealth;

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
        }*/

        krisCurrentMaxHealth = krisMaxHealth;

        krisCurrentHealth = krisMaxHealth;
    }

    void Update()
    {
        CheckHealth();
        UpdateSheildSlider(currentShield);
    }

    void CheckHealth()
    {
        if (isKrisActive)
        {
            if (krisCurrentHealth <= 0)
            {
                isKrisDead = true;
                changePlayerScript.ChangeToTrip();
            }
            else
            {
                isKrisDead = false;
                UpdateHPSlider(krisCurrentHealth);
            }
        }

        if (krisCurrentHealth > krisCurrentMaxHealth)
        {
            krisCurrentHealth = krisCurrentMaxHealth;
        }

    }

    void LoadHP()
    {

        if (isKrisActive)
        {
            krisCurrentHealth = PlayerPrefs.GetInt("KrisHP");
            sliderValueHP.value = krisCurrentHealth;
            UpdateHPSlider(krisCurrentHealth);
        }

    }

    public void SaveHP()
    {
        PlayerPrefs.SetInt("KrisHP", krisCurrentHealth);
    }

    void UpdateHPSlider(int currentHealth)
    {

        if (isKrisActive)
        {
            sliderValueHP.maxValue = krisCurrentMaxHealth;
            sliderValueHP.value = currentHealth;
            //SaveHP();
        }
    }

    void UpdateSheildSlider(int ss)
    {
        sliderValueShield.maxValue = krisMaxHealth;
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
            if (isKrisActive)
            {
                if (currentShield > 0)
                {
                    currentShield -= damage;
                }
                else
                {
                    krisCurrentHealth -= damage;
                }

            }
        }
    }

    public void Heal(int heal)
    {
        if (isKrisActive)
        {
            krisCurrentHealth += heal;
        }

    }

    public void MakeAShield(int ss)
    {

        shieldStrength = ss;
        currentShield += shieldStrength;
        if (currentShield > krisMaxHealth)
        {
            currentShield = krisMaxHealth;
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

    /*
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
    }*/


}
