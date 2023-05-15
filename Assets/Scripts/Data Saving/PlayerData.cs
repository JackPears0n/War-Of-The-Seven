using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //-------------------------
    // Modifier Variables
    //-------------------------
    public int tripMaxHPMod;
    public int krisMaxHPMod;

    public int[] tripSkillMods;
    public int[] krisSkillMods;

    public int maxTokens;
    public int currentTokens;
    public int usedTokens;

    //-------------------------
    // Player Object Variables
    //-------------------------
    public float[] position;

    public PlayerData (StatScript ss, PlayerMovement pm)
    {
        // HP
        tripMaxHPMod = ss.tripMaxHPMod;
        krisMaxHPMod = ss.krisMaxHPMod;

        // Skill points
        tripSkillMods = new int[4];
        tripSkillMods[0] = ss.tripSkillMods[0];
        tripSkillMods[1] = ss.tripSkillMods[1];
        tripSkillMods[2] = ss.tripSkillMods[2];
        tripSkillMods[3] = ss.tripSkillMods[3];

        krisSkillMods = new int[4];
        krisSkillMods[0] = ss.krisSkillMods[0];
        krisSkillMods[1] = ss.krisSkillMods[1];
        krisSkillMods[2] = ss.krisSkillMods[2];
        krisSkillMods[3] = ss.krisSkillMods[3];

        // Skill point tokens
        maxTokens = ss.maxTokens;
        currentTokens = ss.currentTokens;
        usedTokens = ss.usedTokens;

        // Player position
        position = new float[4];
        position[0] = pm.pCS.transform.position.x;
        position[1] = pm.pCS.transform.position.y;
        position[2] = pm.pCS.transform.position.z;

    }
}
