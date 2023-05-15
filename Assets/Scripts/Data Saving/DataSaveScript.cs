using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DataSaveScript : MonoBehaviour
{
    public GameObject playerObj;

    private StatScript ss;
    private PlayerMovement pm;

    private void Start()
    {
        ss = GetComponent<StatScript>();
        pm = playerObj.GetComponent<PlayerMovement>();
    }
    public void SaveData()
    {
        SaveSystem.SavePlayer(ss, pm);
    }

    public void LoadPLayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        // HP
        ss.tripMaxHPMod = data.tripMaxHPMod;
        ss.krisMaxHPMod = data.krisMaxHPMod;

        //Skill points
        ss.tripSkillMods[0] = data.tripSkillMods[0];
        ss.tripSkillMods[1] = data.tripSkillMods[1];
        ss.tripSkillMods[2] = data.tripSkillMods[2];
        ss.tripSkillMods[3] = data.tripSkillMods[3];

        ss.krisSkillMods[0] = data.krisSkillMods[0];
        ss.krisSkillMods[1] = data.krisSkillMods[1];
        ss.krisSkillMods[2] = data.krisSkillMods[2];
        ss.krisSkillMods[3] = data.krisSkillMods[3];

        // Skill point tokens
        ss.maxTokens = data.maxTokens;
        ss.currentTokens = data.currentTokens;
        ss.usedTokens = data.usedTokens;

        // Player pos
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];       
        playerObj.transform.position = new Vector3(data.position[0], 
            data.position[1], data.position[2]);
    }
}
