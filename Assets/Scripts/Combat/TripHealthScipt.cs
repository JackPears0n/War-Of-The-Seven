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

public class TripHealthScipt : MonoBehaviour
{
    //-----------
    // Variables
    //-----------
    [Header("Bools")]
    public bool dead;

    // Health values
    [Header("Health values")]
    public int currentHealth;
    public int maxHealth;

    // HP slider
    [Header("Slider")]
    [SerializeField] Slider sliderValueHP = null;

    void Start()
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

    void Update()
    {
        CheckHealth();
    }

    void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            dead = true;
        }
        else
        {
            dead = false;
            SaveHP();
        }
    }

    void LoadHP()
    {
        currentHealth = PlayerPrefs.GetInt("TripHP");
        sliderValueHP.value = currentHealth;
        UpdateHPSlider(currentHealth);
    }

    public void SaveHP()
    {
        PlayerPrefs.SetInt("TripHP", currentHealth);
    }

    void UpdateHPSlider(int currentHealth)
    {
        sliderValueHP.value = currentHealth;
        SaveHP();
    }

    void Die()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }

    void LoseHealth(int damage)
    {
        currentHealth -= damage;
    }

    void Heal(int heal)
    {
        currentHealth += heal;
    }
}
