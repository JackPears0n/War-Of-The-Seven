using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProgressionScript : MonoBehaviour
{
    private PlayerTalkScript pTS;
    public TMP_Text objective;
    public TMP_Text dialouge;
    public GameObject player;

    public bool hasLookedAround;
    public bool talkedToKris;

    public bool helpedKris;
    public bool lookingForCog;
    public bool foundVoidCrawler;
    public bool killedVoidCrawler;

    public bool killedTheBoss;
    public bool foundTheBoss;
    public bool exitUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        pTS = player.GetComponent<PlayerTalkScript>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckProgress();
    }

    void CheckProgress()
    {
        if (!talkedToKris)
        {
            LookAround();
        }

        else if (!helpedKris)
        {
            HelpKris();
        }

        else if (!exitUnlocked)
        {
            KillTheGateKeeper();
        }
    }

    void LookAround()
    {
        // Find Kris
        if (!talkedToKris)
        {
            objective.text = "Objective: \n Look around for clues to your surroundings";
        }

        // talk to Kris
        // set talkedToKris to true
        // set hasLookedAround to true
        // lookingForCog is true
    }

    void HelpKris()
    {
        if (lookingForCog)
        {
            objective.text = "Objective: \n Follow the path of lights to find Kris' cog";
            
        }

        if (foundVoidCrawler)
        {
            objective.text = "Objective: \n Kill the Crawler and retreive Kris' cog";

        }

        if (killedVoidCrawler)
        {
            objective.text = "Objective: \n Go with Kris to find a way out";

        }
    }

    void KillTheGateKeeper()
    {
        // Find Gate keeper
        if (!foundTheBoss)
        {
            objective.text = "Objective: \n Find the Gate Keeper";
        }

        // Kill Gate keeper
        if (foundTheBoss && !killedTheBoss)
        {
            objective.text = "Objective: \n Slay the Gate Keeper";
        }

        // Unlock exit
        if (killedTheBoss)
        {
            objective.text = "Objective: \n Find the exit on the Gate Keeper's island";
            exitUnlocked = true;
        }
    }
}
