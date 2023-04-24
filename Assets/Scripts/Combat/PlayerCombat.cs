using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    States state;

    private EnemyHealthScript eHS;
    private PlayerHealthScript pHS;

    public Transform attackPoint;

    [Header("Layers")]
    public LayerMask enemyLayers;


    [Header("Damage Variables")]
    public int basicSkillDmg;
    public int advancedSkillDmg;
    public int tuningSkillDmg;
    public int pinnacleSkillDmg;

    [Header("Attack Range Variables")]
    public int basicSkillRange;
    public int advancedSkillRange;
    public int tuningSkillRange;
    public int pinnacleSkillRange;

    [Header("Cooldown Variables")]
    public float basicSkillCool;
    public float advancedSkillCool;
    public float tuningSkillCool;
    public float pinnacleSkillCool;

    [Header("Stamina Variables")]
    public float stamina;
    public float basicSkillStamCost;
    public float advancedSkillStamCost;
    public float tuningSkillStamCost;
    public float pinnacleSkillStamCost;

    [Header("Veil Variables")]
    public float veil;
    public float basicSkillVeilCost;
    public float advancedSkillVeilCost;
    public float tuningSkillVeilCost;
    public float pinnacleSkillVeilCost;


    // Start is called before the first frame update
    void Start()
    {
        eHS = GetComponent<EnemyHealthScript>();
        pHS = GetComponent<PlayerHealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();

    }

    void CheckState()
    {
        if (pHS.isTripActive)
        {
            // Damage
            basicSkillDmg = 5;
            advancedSkillDmg = 15;
            tuningSkillDmg = 0;
            pinnacleSkillDmg = 40;

            // Range
            basicSkillRange = 10;
            advancedSkillRange = 1;
            tuningSkillRange = 100;
            pinnacleSkillRange = 1;

            // Cooldowns
            basicSkillCool = 0.5f;
            advancedSkillCool = 2;
            tuningSkillCool = 30;
            pinnacleSkillCool = 120;

            // Stamina costs
            basicSkillStamCost = 5;
            advancedSkillStamCost = 10;
            tuningSkillStamCost = 0;
            pinnacleSkillStamCost = 20;

            TripAttacks();
        }

        /*if (state == States.Kris)
        {
            basicSkillDmg = 5;
            advancedSkillDmg = 20;
            tuningSkillDmg = 10;
            pinnacleSkillDmg = 0;
        }*/
    }

    void TripAttacks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, basicSkillRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(basicSkillDmg);
            }
        }

        // Pinnacle
        if (Input.GetKeyDown(KeyCode.Q))
        {

        }

        // Advanced
        if (Input.GetKeyDown(KeyCode.E))
        {

        }

        // Tuning
        if (Input.GetKeyDown(KeyCode.R))
        {

        }
    }

    void KrisAttacks()
    {

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, basicSkillRange);
        Gizmos.DrawWireSphere(attackPoint.position, advancedSkillRange);
        Gizmos.DrawWireSphere(attackPoint.position, tuningSkillRange);
        Gizmos.DrawWireSphere(attackPoint.position, pinnacleSkillRange);
    }
}
