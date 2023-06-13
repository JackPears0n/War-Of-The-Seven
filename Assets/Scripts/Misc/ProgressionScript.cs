using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressionScript : MonoBehaviour
{
    public GameObject[] guideObjects;
    // Location 1 (0)
    private bool o0hbr = false;
    int o = 0;

    private PlayerTalkScript pTS;
    public TMP_Text objective;
    public TMP_Text dialouge;
    public GameObject player;
    public ChangePlayerScript changePlayerScript;
    public GameObject krisNPC;

    public bool hasLookedAround;
    public bool talkedToKris;

    public bool helpedKris;
    public bool lookingForCog;
    public bool foundVoidCrawler;
    public GameObject voidCrawlerTrigger;
    public GameObject voidCrawler;
    public bool killedVoidCrawler;
    public GameObject teleportButton;

    public bool killedTheBoss;
    public GameObject bossIslandTigger;
    public bool foundBossIsland;
    public bool foundTheBoss;
    public GameObject bossTrigger;
    public GameObject boss;
    public bool exitUnlocked;
    public GameObject voidFissure;

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

            // If the first marker is not active move to the next one
            if (!guideObjects[o].activeSelf && !o0hbr)
            {
                o = 1;
                guideObjects[o].SetActive(true);
                o0hbr = true;
            }

            else if (!guideObjects[o].activeSelf && o0hbr && o < 1)
            {
                o++;
                guideObjects[o].SetActive(true);
            }
            LookAround();
          
        }

        else if (!helpedKris)
        {
            if (!guideObjects[o].activeSelf && o0hbr && o < 44)
            {
                o++;
                guideObjects[o].SetActive(true);
            }
            HelpKris();
        }

        else if (!exitUnlocked)
        {
            if (!guideObjects[o].activeSelf && o0hbr)
            {
                o++;
                guideObjects[o].SetActive(true);
            }
            KillTheGateKeeper();
        }
    }

    void LookAround()
    {
        // Find Kris
        if (!talkedToKris)
        {
            objective.text = "Objective: \n Look around for clues to your surroundings. \n Follow the yellow arrows pointing at the ground if you need help navigating";
        }

        // talk to Kris
        // set talkedToKris to true
        // set hasLookedAround to true
        // lookingForCog is true
    }

    void HelpKris()
    {
        if (lookingForCog && !foundVoidCrawler)
        {
            objective.text = "Objective: \n Follow the path of lights, in the direction Kris was facing, to find Kris' cog";
            if (voidCrawlerTrigger.activeSelf == false)
            {
                foundVoidCrawler = true;
            }
            
        }

        if (foundVoidCrawler && !killedVoidCrawler)
        {
            dialouge.gameObject.SetActive(true);
            dialouge.text = "Look in that cavern friend, that abhorrent brute has one's cog. " +
                "It seems agressive, looks like we must retrieve it by force.";
            objective.text = "Objective: \n Kill the Crawler and retreive Kris' cog";
            if (voidCrawler.activeSelf == false)
            {
                killedVoidCrawler = true;
            }
        }

        if (killedVoidCrawler)
        {           
            objective.text = "Objective: \n Go with Kris to find the gate keeper's island";
            dialouge.text = "Thank you very much friend, now, let us continue on with this quest! " +
                "Here, let me grant you the ability to take us both to the gate keeper's island";
            teleportButton.SetActive(true);
            helpedKris = true;
        }
    }

    void KillTheGateKeeper()
    {
        if (bossIslandTigger.activeSelf == false)
        {
            foundBossIsland = true;
        }
        
        // Find Gate keeper
        if (foundBossIsland && !foundTheBoss)
        {
            dialouge.gameObject.SetActive(false);
            objective.text = "Objective: \n Find the Gate Keeper";
            if (bossTrigger.activeSelf == false)
            {
                foundTheBoss = true;
            }
        }

        // Kill Gate keeper
        if (foundTheBoss && !killedTheBoss)
        {
            objective.text = "Objective: \n Slay the Gate Keeper";
            if (boss.activeSelf == false)
            {
                killedTheBoss = true;
            }
        }

        // Unlock exit
        if (killedTheBoss && !exitUnlocked)
        {
            objective.text = "Objective: \n Find the exit on the Gate Keeper's island";
            exitUnlocked = true;
            voidFissure.SetActive(true);
        }

        if (exitUnlocked)
        {
            if (voidFissure.activeSelf == false)
            {
                SceneManager.LoadScene("Completed");
            }
        }
    }
}
