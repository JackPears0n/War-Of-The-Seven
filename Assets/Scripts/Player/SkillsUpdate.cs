using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsUpdate : MonoBehaviour
{
    public GameObject player;
    private PlayerHealthScript pHS;
    private PlayerCombat pCS;

    public GameObject[] tripSkills;
    public GameObject[] krisSkills;
    public GameObject[] skillIndicator;
    int skillNum;

    // Start is called before the first frame update
    void Start()
    {
        pHS = player.GetComponent<PlayerHealthScript>();
        pCS = player.GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeIcon();
    }

    private void ChangeIcon()
    {
        if (pHS.isTripActive)
        {
            foreach (GameObject i in tripSkills)
            {
                i.SetActive(true);
            }
            foreach (GameObject i in krisSkills)
            {
                i.SetActive(false);
            }
        }

        if (pHS.isKrisActive)
        {
            foreach (GameObject i in tripSkills)
            {
                i.SetActive(false);
            }
            foreach (GameObject i in krisSkills)
            {
                i.SetActive(true);
            }
        }

        #region skillIndicators
        foreach (GameObject i in skillIndicator)
        {
            skillNum++;

            if (skillNum > 3 || skillNum < 0)
            {
                skillNum = 0;
            }

            if (pHS.isTripActive)
            {   
                if (pCS.tripSkillsReady[skillNum])
                {
                    skillIndicator[skillNum].SetActive(true);
                }
                else
                {
                    skillIndicator[skillNum].SetActive(false);
                }
            }

            if (pHS.isKrisActive)
            {
                if (pCS.krisSkillsReady[skillNum])
                {
                    skillIndicator[skillNum].SetActive(true);
                }
                else
                {
                    skillIndicator[skillNum].SetActive(false);
                }
            }        

        }

        #endregion
    }
}
