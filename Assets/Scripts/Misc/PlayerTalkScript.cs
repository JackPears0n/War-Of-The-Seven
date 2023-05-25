using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalkScript : MonoBehaviour
{
    private ProgressionScript pS;
    public GameObject gameManager;
    public Transform player;
    public LayerMask npcs;
    public Camera cam;
    public float talkRange;

    // Start is called before the first frame update
    void Start()
    {
        pS = gameManager.GetComponent<ProgressionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            Collider[] hitNPCS = Physics.OverlapSphere(player.position, talkRange, npcs);

            foreach (Collider npc in hitNPCS)
            {
                if (npc.gameObject.tag == "Kris")
                {
                    pS.dialouge.text = "Well met friend, my name is Kris. " +
                        "One presume you fell down here too? Yes? Well, The Void, where we are, is not somewhere one wants to be in for very long. If you help to get one's cog back, one can help you in turn. " +
                        "Don't worry, oneself help you get it back, do not fret. Just follow the Void Lights and we shall find one's misssing cog.";
                    pS.talkedToKris = true;
                    pS.hasLookedAround = true;
                    pS.lookingForCog = true;
                }
                else
                {
                    return;
                }
            }


        }
    }
}
