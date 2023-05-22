using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthScript : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    [SerializeField]public Slider healthBar;


    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Void Orb")
        {
            maxHealth = 25;
            currentHealth = maxHealth;
        }

        if (gameObject.tag == "Void Crawler")
        {
            maxHealth = 200;
            currentHealth = maxHealth;
        }

        if (gameObject.tag == "Corrupted Crystal Construct")
        {
            maxHealth = 1000;
            currentHealth = maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    void CheckHealth()
    {
        UpdateHPBar();
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
    }

    public void HalveHP()
    {
        currentHealth /= 2;
    }

    public void UpdateHPBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }
}
