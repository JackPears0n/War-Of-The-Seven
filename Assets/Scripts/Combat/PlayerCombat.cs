using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{
    States state;

    private EnemyHealthScript eHS;
    private PlayerHealthScript pHS;

    private bool drawGizmos;

    public Transform tripAttackPoint;
    public Transform krisAttackPoint;

    public bool tripIsAlreadyActive;
    public bool krisIsAlreadyActive;

    [Header("Layers")]
    public LayerMask enemyLayers;

    [Header("Damage Variables")]
    public int tripbasicSkillDmg;
    public int tripadvancedSkillDmg;
    public int triptuningSkillDmg;
    public int trippinnacleSkillDmg;
    public int[] tripDamage;

    public int krisbasicSkillDmg;
    public int krisadvancedSkillDmg;
    public int kristuningSkillDmg;
    public int krispinnacleSkillDmg;

    [Header("Attack Range Variables")]
    public int tripbasicSkillRange;
    public int tripadvancedSkillRange;
    public int triptuningSkillRange;
    public int trippinnacleSkillRange;

    public int krisbasicSkillRange;
    public int krisadvancedSkillRange;
    public int kristuningSkillRange;
    public int krispinnacleSkillRange;

    [Header("Cooldown Variables")]
    public float tripadvancedSkillCool;
    public float triptuningSkillCool;
    public float trippinnacleSkillCool;
    public bool tripadvancedSkillReady;
    public bool triptuningSkillReady;
    public bool trippinnacleSkillReady;
    public float trippinnacleDuration;
    public bool voidPulses;

    public float krisadvancedSkillCool;
    public float kristuningSkillCool;
    public float krispinnacleSkillCool;
    public bool krisadvancedSkillReady;
    public bool kristuningSkillReady;
    public bool krispinnacleSkillReady;
    public float krispinnacleDuration;

    /*    [Header("Stamina Variables")]
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
    */

    [Header("Projectile")]
    public GameObject krisSpear;
    public float krisSpearSpeed;
    public GameObject crystalSpear;
    public float crystalSpearSpeed;

    [Header("Kris variables")]
    private int maxCrystalSpearCharges = 3;
    private int currentCrystalSpearCharges;
    public bool hasCrystalSpearCharge;


    // Start is called before the first frame update
    void Start()
    {
        eHS = GetComponent<EnemyHealthScript>();
        pHS = GetComponent<PlayerHealthScript>();

        drawGizmos = false;

        tripadvancedSkillReady = true;
        triptuningSkillReady = true;
        trippinnacleSkillReady = true;

        krisadvancedSkillReady = true;
        kristuningSkillReady = true;
        krispinnacleSkillReady = true;

        voidPulses = false;
        maxCrystalSpearCharges = 3;
        SetBaseSkillStats();
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
            TripAttacks();
        }

        if (pHS.isKrisActive)
        {
            KrisAttacks();
        }
    }

    void TripAttacks()
    {
        // Basic
        if (Input.GetMouseButtonDown(0) && !voidPulses)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(tripAttackPoint.position, tripbasicSkillRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                //print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(tripbasicSkillDmg);
            }
        }

        // Void Pulses Basic
        if (Input.GetMouseButtonDown(0) && voidPulses)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(tripAttackPoint.position, tripbasicSkillRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                //print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(trippinnacleSkillDmg);
            }
        }

        // Advanced
        if (Input.GetKeyDown(KeyCode.Q) && tripadvancedSkillReady)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(tripAttackPoint.position, tripadvancedSkillRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                //print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(tripadvancedSkillDmg);
            }

            tripadvancedSkillReady = false;
            Invoke(nameof(ResetCooldownAt), tripadvancedSkillCool);
        }

        // Tuning
        if (Input.GetKeyDown(KeyCode.E) && triptuningSkillReady)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(tripAttackPoint.position, tripbasicSkillRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                //print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().HalveHP();
            }

            triptuningSkillReady = false;
            Invoke(nameof(ResetCooldownTt), triptuningSkillCool);
        }

        // Pinnacle
        if (Input.GetKeyDown(KeyCode.R) && trippinnacleSkillReady)
        {
            voidPulses = true;

            trippinnacleSkillReady = false;
            Invoke(nameof(ResetCooldownPt), trippinnacleSkillCool);
        }

    }

    
    void KrisAttacks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Instanciates the spear gameobject
            GameObject CreateSpear = Instantiate(krisSpear, krisAttackPoint.position, krisAttackPoint.rotation);

            // Array for storing enemies
            Collider[] hitEnemies = Physics.OverlapSphere(krisAttackPoint.position, krisbasicSkillRange, enemyLayers);

            // Makes spear move
            CreateSpear.GetComponent<Rigidbody>().velocity = krisAttackPoint.transform.up * krisSpearSpeed;

            // Damages the hit enemies
            foreach (Collider enemy in hitEnemies)
            {
                print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(krisbasicSkillDmg);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && krisadvancedSkillReady)
        {
            pHS.MakeAShield(50);

            Collider[] hitEnemies = Physics.OverlapSphere(krisAttackPoint.position, krisadvancedSkillRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(krisadvancedSkillDmg);
            }

            krisadvancedSkillReady = false;
            Invoke(nameof(ResetCooldownAk), krisadvancedSkillCool);
        }

        if (Input.GetKeyDown(KeyCode.E) && kristuningSkillReady)
        {
            
            for (currentCrystalSpearCharges = maxCrystalSpearCharges; currentCrystalSpearCharges > 0; currentCrystalSpearCharges--)
            {
                // Instanciates the spear gameobject
                GameObject CreateSpear = Instantiate(crystalSpear, krisAttackPoint.position, krisAttackPoint.rotation);

                // Array for storing enemies
                Collider[] hitEnemies = Physics.OverlapSphere(krisAttackPoint.position, kristuningSkillRange, enemyLayers);

                // Makes spear move
                CreateSpear.GetComponent<Rigidbody>().velocity = krisAttackPoint.transform.up * crystalSpearSpeed;

                // Damages the hit enemies
                foreach (Collider enemy in hitEnemies)
                {
                    print("We hit" + enemy.name);
                    enemy.GetComponent<EnemyHealthScript>().TakeDamage(kristuningSkillDmg);
                }
            }
            

            //StartCoroutine("CrystalStorm");

            maxCrystalSpearCharges += 1;

            kristuningSkillReady = false;
            Invoke(nameof(ResetCooldownTk), kristuningSkillCool);
        }

        if (Input.GetKeyDown(KeyCode.R) && krispinnacleSkillReady)
        {
            pHS.Invunerability(krispinnacleDuration);

            if (pHS.invulnerable)
            {
                for (int i = 5; i > 0; i--)
                {
                    pHS.Heal(10);
                }
                
            }

            krispinnacleSkillReady = false;
            Invoke(nameof(ResetCooldownPk), krispinnacleSkillCool);
        }
    }
    
    // Cooldown resets
    void ResetCooldownAt()
    {
        tripadvancedSkillReady = true;
        print("Advanced skill is ready");
    }

    void ResetCooldownTt()
    {

        triptuningSkillReady = true;
        print("Tuning skill is ready");
    }

    void ResetCooldownPt()
    {
        voidPulses = false;

        trippinnacleSkillReady = true;
        print("Pinnacle skill is ready");
    }

    void ResetCooldownAk()
    {
        krisadvancedSkillReady = true;
        print("Advanced skill is ready");
    }

    void ResetCooldownTk()
    {

        kristuningSkillReady = true;
        print("Tuning skill is ready");
    }

    void ResetCooldownPk()
    {
        voidPulses = false;

        krispinnacleSkillReady = true;
        print("Pinnacle skill is ready");
    }

    private void OnDrawGizmosSelected()
    {
        if (tripAttackPoint == null)
        {
            return;
        }
        else
        {
            drawGizmos = true;
        }

        if(tripAttackPoint)
        {
            Gizmos.DrawWireSphere(tripAttackPoint.position, tripbasicSkillRange);
            Gizmos.DrawWireSphere(tripAttackPoint.position, tripadvancedSkillRange);
            Gizmos.DrawWireSphere(tripAttackPoint.position, triptuningSkillRange);
            Gizmos.DrawWireSphere(tripAttackPoint.position, trippinnacleSkillRange);
        }

        if (krisAttackPoint)
        {
            Gizmos.DrawWireSphere(krisAttackPoint.position, krisbasicSkillRange);
            Gizmos.DrawWireSphere(krisAttackPoint.position, krisadvancedSkillRange);
            Gizmos.DrawWireSphere(krisAttackPoint.position, kristuningSkillRange);
            Gizmos.DrawWireSphere(krisAttackPoint.position, krispinnacleSkillRange);
        }

    }

    void SetBaseSkillStats()
    {
        //-----------------
        // Trip
        //-----------------

        // Damage
        tripbasicSkillDmg = 5;
        tripadvancedSkillDmg = 15;
        triptuningSkillDmg = 0;
        trippinnacleSkillDmg = 40;

        // Range
        tripbasicSkillRange = 10;
        tripadvancedSkillRange = 1;
        triptuningSkillRange = 100;
        trippinnacleSkillRange = 1;

        // Cooldowns
        tripadvancedSkillCool = 2;
        triptuningSkillCool = 30;
        trippinnacleSkillCool = 120;

        trippinnacleDuration = 30;
        /*
        // Stamina costs
        tripbasicSkillStamCost = 5;
        tripadvancedSkillStamCost = 10;
        triptuningSkillStamCost = 0;
        trippinnacleSkillStamCost = 20;
        */
        //-----------------
        // Kris
        //-----------------

        // Damage
        krisbasicSkillDmg = 5;
        krisadvancedSkillDmg = 30;
        kristuningSkillDmg = 10;
        krispinnacleSkillDmg = 0;

        // Range
        krisbasicSkillRange = 100;
        krisadvancedSkillRange = 10;
        kristuningSkillRange = 100;
        krispinnacleSkillRange = 0;

        // Cooldowns
        krisadvancedSkillCool = 10;
        kristuningSkillCool = 20;
        krispinnacleSkillCool = 120;

        krispinnacleDuration = 30;
        /*
        // Stamina costs
        krisbasicSkillStamCost = 5;
        krisadvancedSkillStamCost = 10;
        kristuningSkillStamCost = 0;
        krispinnacleSkillStamCost = 20;
        */
    }
}
