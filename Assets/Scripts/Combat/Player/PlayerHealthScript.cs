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
    private ChangePlayerScript changePlayerScript;

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
    public int tripCurrentMaxHealth;
    public int krisCurrentMaxHealth;
    public int tripMaxHealth;
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

        tripCurrentMaxHealth = tripMaxHealth;
        krisCurrentMaxHealth = krisMaxHealth;

        tripCurrentHealth = tripMaxHealth;
        krisCurrentHealth = krisMaxHealth;

        changePlayerScript = GetComponent<ChangePlayerScript>();
    }

    void Update()
    {
        CheckHealth();
        UpdateSheildSlider(currentShield);
    }

    void CheckHealth()
    {        
        if (krisCurrentHealth <= 0 && tripCurrentHealth <= 0)
        {
            state = States.Dead;
            Die();
        }

        if (isTripActive)
        {
            if (tripCurrentHealth <= 0 && changePlayerScript.enabled == false)
            {
                isTripDead = true;
                state = States.Dead;
                Die();
            }
            else if (tripCurrentHealth <= 0)
            {
                isTripDead = true;
                changePlayerScript.ChangeToKris();
            }
            else
            {
                isTripDead = false;
                UpdateHPSlider(tripCurrentHealth);
            }
        }

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

        if (tripCurrentHealth > tripCurrentMaxHealth)
        {
            tripCurrentHealth = tripCurrentMaxHealth;
        }

        if (krisCurrentHealth > krisCurrentMaxHealth)
        {
            krisCurrentHealth = krisCurrentMaxHealth;
        }

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
        if (isTripActive)
        {
            sliderValueHP.maxValue = tripCurrentMaxHealth;
            sliderValueHP.value = currentHealth;
            //SaveHP();
        }

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
            if (isTripActive)
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

        if (isTripActive)
        {
            tripCurrentHealth += heal;
        }

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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            state = States.Dead;
            Die();
        }
    }
}
