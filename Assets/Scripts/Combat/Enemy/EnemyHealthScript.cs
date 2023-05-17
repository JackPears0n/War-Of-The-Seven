using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;


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
}
