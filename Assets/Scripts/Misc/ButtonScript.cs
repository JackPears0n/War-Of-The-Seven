using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject playerObj;
    private StatScript ss;

    [Header("Text")]
    public TMP_Text tripHPTokens;
    public TMP_Text krisHPTokens;

    public TMP_Text[] tripSkillTokens;
    public TMP_Text[] krisSkillTokens;

    // Start is called before the first frame update
    void Start()
    {
        ss = playerObj.GetComponent<StatScript>();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void AddModTokenHP(string modType)
    {
        if (modType == "TripMaxHP")
        {
            if (ss.tripMaxHPMod < 10)
            {
                ss.tripMaxHPMod += 1;
                tripHPTokens.text = ss.tripMaxHPMod.ToString();
            }
            else
            {
                tripHPTokens.text = ss.tripMaxHPMod.ToString();
                return;
            }
        }
        if (modType == "KrisMaxHP")
        {
            if (ss.krisMaxHPMod < 10)
            {
                ss.krisMaxHPMod += 1;
                krisHPTokens.text = ss.krisMaxHPMod.ToString();
            }
            else
            {
                krisHPTokens.text = ss.krisMaxHPMod.ToString();
                return;
            }
        }

    }

    public void RemoveModTokenHP(string modType)
    {
        if (modType == "TripMaxHP")
        {
            if (ss.tripMaxHPMod > 0)
            {
                ss.tripMaxHPMod -= 1;
                tripHPTokens.text = ss.tripMaxHPMod.ToString();
            }
            else
            {
                tripHPTokens.text = ss.tripMaxHPMod.ToString();

                return;
            }
            
        }
        if (modType == "KrisMaxHP")
        {
            if (ss.krisMaxHPMod > 0)
            {
                ss.krisMaxHPMod -= 1;
                krisHPTokens.text = ss.krisMaxHPMod.ToString();
            }
            else
            {
                krisHPTokens.text = ss.krisMaxHPMod.ToString();
                return;
            }
        }
    }

    public void AddModTokenSkill(string characterandskill)
    {
        // Trip
        if (characterandskill == "trip0")
        {
            
            if (ss.tripSkillMods[0] < 10)
            {
                ss.tripSkillMods[0] += 1;
                tripSkillTokens[0].text = ss.tripSkillMods[0].ToString();
            }
            else
            {
                tripSkillTokens[0].text = ss.tripSkillMods[0].ToString();
                return;
            }

        }

        if (characterandskill == "trip1")
        {

            if (ss.tripSkillMods[1] < 10)
            {
                ss.tripSkillMods[1] += 1;
                tripSkillTokens[1].text = ss.tripSkillMods[1].ToString();
            }
            else
            {
                tripSkillTokens[1].text = ss.tripSkillMods[1].ToString();
                return;
            }

        }

        if (characterandskill == "trip2")
        {

            if (ss.tripSkillMods[2] < 10)
            {
                ss.tripSkillMods[2] += 1;
                tripSkillTokens[2].text = ss.tripSkillMods[2].ToString();
            }
            else
            {
                tripSkillTokens[2].text = ss.tripSkillMods[2].ToString();
                return;
            }

        }

        if (characterandskill == "trip3")
        {

            if (ss.tripSkillMods[3] < 10)
            {
                ss.tripSkillMods[3] += 1;
                tripSkillTokens[3].text = ss.tripSkillMods[3].ToString();
            }
            else
            {
                tripSkillTokens[3].text = ss.tripSkillMods[3].ToString();
                return;
            }

        }

        // Kris
        if (characterandskill == "kris0")
        {

            if (ss.krisSkillMods[0] < 10)
            {
                ss.krisSkillMods[0] += 1;
                krisSkillTokens[0].text = ss.krisSkillMods[0].ToString();
            }
            else
            {
                krisSkillTokens[0].text = ss.krisSkillMods[0].ToString();
                return;
            }

        }

        if (characterandskill == "kris1")
        {

            if (ss.krisSkillMods[1] < 10)
            {
                ss.krisSkillMods[1] += 1;
                krisSkillTokens[1].text = ss.krisSkillMods[1].ToString();
            }
            else
            {
                krisSkillTokens[1].text = ss.krisSkillMods[1].ToString();
                return;
            }

        }

        if (characterandskill == "kris2")
        {

            if (ss.krisSkillMods[2] < 10)
            {
                ss.krisSkillMods[2] += 1;
                krisSkillTokens[2].text = ss.krisSkillMods[2].ToString();
            }
            else
            {
                krisSkillTokens[2].text = ss.krisSkillMods[2].ToString();
                return;
            }

        }

        if (characterandskill == "kris3")
        {

            if (ss.krisSkillMods[3] < 10)
            {
                ss.krisSkillMods[3] += 1;
                krisSkillTokens[3].text = ss.krisSkillMods[3].ToString();
            }
            else
            {
                krisSkillTokens[3].text = ss.krisSkillMods[3].ToString();
                return;
            }

        }

    }
}
