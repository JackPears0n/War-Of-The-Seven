using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatScript : MonoBehaviour
{
    public GameObject statMenu;

    private PlayerHealthScript pHS;
    private PlayerCombat pCS;

    // HP modifier variables
    public int tripMaxHPMod;
    public int krisMaxHPMod;

    // Skill modifier Arrays
    public int[] tripSkillMods;
    public int[] krisSkillMods;

    // Modifier tokens
    public int maxTokens = 30;
    public int currentTokens;
    public int usedTokens;

    // Start is called before the first frame update
    void Start()
    {
        pHS = GetComponent<PlayerHealthScript>();
        pCS = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
            statMenu.SetActive(!statMenu.activeSelf);
            UseMultipliers();
        }

    }

    public void UseMultipliers()
    {
        // Boost trip max hp
        pHS.tripCurrentMaxHealth = (pHS.tripMaxHealth + (5 * tripMaxHPMod));
        // Boost kris max hp
        pHS.krisCurrentMaxHealth = (pHS.krisMaxHealth + (7 * krisMaxHPMod));

        // Boost trip basic dmg
        pCS.tripDamage[0] += (1 * tripSkillMods[0]);
        // Boost trip advanced basic dmg
        pCS.tripDamage[1] += (2 * tripSkillMods[1]);
        // lower trip tuning cooldown
        pCS.tripCooldowns[2] -= (1 * tripSkillMods[2]);
        // Boost trip pinnacle basic dmg
        pCS.tripDamage[3] += (2 * tripSkillMods[3]);

        // Boost kris basic dmg
        pCS.krisDamage[0] += (krisSkillMods[0]);
        // Boost kris advanced shield
        pCS.krisDamage[1] += (2 * krisSkillMods[1]);
        // lower kris tuning cooldown
        pCS.krisCooldowns[2] -= (1 * krisSkillMods[2]);
        // lower kris pinnacle cooldown
        pCS.krisCooldowns[3] -= (2 * krisSkillMods[3]);
    }
}
