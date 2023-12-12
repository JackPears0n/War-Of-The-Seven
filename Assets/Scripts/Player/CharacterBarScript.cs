using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterBarScript : MonoBehaviour
{
    public GameObject gameManager;
    private ProgressionScript progScr;

    public GameObject player;
    private PlayerHealthScript pHS;
    public GameObject[] characterSprites;
    public Slider[] hpBars;

    public TMP_Text[] names;
    // Start is called before the first frame update
    void Start()
    {
        progScr = gameManager.GetComponent<ProgressionScript>();
        pHS = player.GetComponent<PlayerHealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Makes the Kris bar viewable
        if (progScr.talkedToKris)
        {
            characterSprites[1].SetActive(true);
        }

        // Makes Trip's name green and Kris' white
        if (pHS.isTripActive)
        {
            names[0].GetComponent<TMP_Text>().color = Color.green;
            names[1].GetComponent<TMP_Text>().color = Color.white;
        }

        // Makes Kris' name green and Trip's white
        if (pHS.isKrisActive)
        {
            names[0].GetComponent<TMP_Text>().color = Color.white;
            names[1].GetComponent<TMP_Text>().color = Color.green;
        }

        // Updates the HP bars
        hpBars[0].maxValue = pHS.tripMaxHealth;
        hpBars[0].value = pHS.tripCurrentHealth;

        hpBars[1].maxValue = pHS.krisMaxHealth;
        hpBars[1].value = pHS.krisCurrentHealth;
    }
}
