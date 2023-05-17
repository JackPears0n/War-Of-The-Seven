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
    public int[] tripDamage;
    public int[] krisDamage;

    [Header("Attack Range Variables")]   
    public int[] tripRange;
    public int[] krisRange;

    [Header("Cooldown Variables")]
    // Trip
    public float[] tripCooldowns;
    public bool[] tripSkillsReady;
    public float trippinnacleDuration;
    public bool voidPulses;

    // Kris
    public float[] krisCooldowns;
    public bool[] krisSkillsReady;
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

        tripSkillsReady[1] = true;
        tripSkillsReady[2] = true;
        tripSkillsReady[3] = true;

        krisSkillsReady[1] = true;
        krisSkillsReady[2] = true;
        krisSkillsReady[3] = true;

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
            Collider[] hitEnemies = Physics.OverlapSphere(tripAttackPoint.position, tripRange[0], enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                //print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(tripDamage[0]);
            }
        }

        // Void Pulses Basic
        if (Input.GetMouseButtonDown(0) && voidPulses)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(tripAttackPoint.position, tripRange[0], enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                //print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(tripDamage[3]);
            }
        }

        // Advanced
        if (Input.GetKeyDown(KeyCode.Q) && tripSkillsReady[1])
        {
            Collider[] hitEnemies = Physics.OverlapSphere(tripAttackPoint.position, tripRange[1], enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                //print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(tripDamage[1]);
            }

            tripSkillsReady[1] = false;
            Invoke(nameof(ResetCooldownAt), tripCooldowns[1]);
        }

        // Tuning
        if (Input.GetKeyDown(KeyCode.E) && tripSkillsReady[2])
        {
            Collider[] hitEnemies = Physics.OverlapSphere(tripAttackPoint.position, tripRange[2], enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                //print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().HalveHP();
            }

            tripSkillsReady[2] = false;
            Invoke(nameof(ResetCooldownTt), tripCooldowns[2]);
        }

        // Pinnacle
        if (Input.GetKeyDown(KeyCode.R) && tripSkillsReady[3])
        {
            voidPulses = true;

            tripSkillsReady[3] = false;
            Invoke(nameof(ResetCooldownPt), tripCooldowns[3]);
        }

    }
    
    void KrisAttacks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Instanciates the spear gameobject
            GameObject CreateSpear = Instantiate(krisSpear, krisAttackPoint.position, krisAttackPoint.rotation);

            // Array for storing enemies
            Collider[] hitEnemies = Physics.OverlapSphere(krisAttackPoint.position, krisRange[0], enemyLayers);

            // Makes spear move
            CreateSpear.GetComponent<Rigidbody>().velocity = krisAttackPoint.transform.up * krisSpearSpeed;

            // Damages the hit enemies
            foreach (Collider enemy in hitEnemies)
            {
                print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(krisDamage[0]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && krisSkillsReady[1])
        {
            pHS.MakeAShield(krisDamage[1]);

            Collider[] hitEnemies = Physics.OverlapSphere(krisAttackPoint.position, krisRange[1], enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(krisDamage[1]);
            }

            krisSkillsReady[1] = false;
            Invoke(nameof(ResetCooldownAk), krisCooldowns[1]);
        }

        if (Input.GetKeyDown(KeyCode.E) && krisSkillsReady[2])
        {
            
            for (currentCrystalSpearCharges = maxCrystalSpearCharges; currentCrystalSpearCharges > 0; currentCrystalSpearCharges--)
            {
                // Instanciates the spear gameobject
                GameObject CreateSpear = Instantiate(crystalSpear, krisAttackPoint.position, krisAttackPoint.rotation);

                // Array for storing enemies
                Collider[] hitEnemies = Physics.OverlapSphere(krisAttackPoint.position, krisRange[2], enemyLayers);

                // Makes spear move
                CreateSpear.GetComponent<Rigidbody>().velocity = krisAttackPoint.transform.up * crystalSpearSpeed;

                // Damages the hit enemies
                foreach (Collider enemy in hitEnemies)
                {
                    print("We hit" + enemy.name);
                    enemy.GetComponent<EnemyHealthScript>().TakeDamage(krisDamage[2]);
                }
            }
            

            //StartCoroutine("CrystalStorm");

            maxCrystalSpearCharges += 1;

            krisSkillsReady[2] = false;
            Invoke(nameof(ResetCooldownTk), krisCooldowns[2]);
        }

        if (Input.GetKeyDown(KeyCode.R) && krisSkillsReady[3])
        {
            pHS.Invunerability(krispinnacleDuration);

            if (pHS.invulnerable)
            {
                for (int i = 5; i > 0; i--)
                {
                    pHS.Heal(10);
                }
                
            }

            krisSkillsReady[3] = false;
            Invoke(nameof(ResetCooldownPk), krisCooldowns[3]);
        }
    }
    
    // Cooldown resets
    void ResetCooldownAt()
    {
        tripSkillsReady[1] = true;
        print("Advanced skill is ready");
    }

    void ResetCooldownTt()
    {

        tripSkillsReady[2] = true;
        print("Tuning skill is ready");
    }

    void ResetCooldownPt()
    {
        voidPulses = false;

        tripSkillsReady[3] = true;
        print("Pinnacle skill is ready");
    }

    void ResetCooldownAk()
    {
        krisSkillsReady[1] = true;
        print("Advanced skill is ready");
    }

    void ResetCooldownTk()
    {

        krisSkillsReady[2] = true;
        print("Tuning skill is ready");
    }

    void ResetCooldownPk()
    {
        voidPulses = false;

        krisSkillsReady[3] = true;
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
        /*
        if(pHS.isTripActive)
        {
            Gizmos.DrawWireSphere(tripAttackPoint.position, tripRange[0]);
            Gizmos.DrawWireSphere(tripAttackPoint.position, tripRange[1]);
            Gizmos.DrawWireSphere(tripAttackPoint.position, tripRange[2]);
            Gizmos.DrawWireSphere(tripAttackPoint.position, tripRange[3]);
        }

        if (pHS.isKrisActive)
        {
            Gizmos.DrawWireSphere(krisAttackPoint.position, krisRange[0]);
            Gizmos.DrawWireSphere(krisAttackPoint.position, krisRange[1]);
            Gizmos.DrawWireSphere(krisAttackPoint.position, krisRange[2]);
            Gizmos.DrawWireSphere(krisAttackPoint.position, krisRange[3]);
        }
        */
    }

    void SetBaseSkillStats()
    {
        //-----------------
        // Trip
        //-----------------

        // Damage
        tripDamage[0] = 5;
        tripDamage[1] = 15;
        tripDamage[2] = 0;
        tripDamage[3] = 40;

        // Range
        tripRange[0] = 10;
        tripRange[1] = 10;
        tripRange[2] = 100;
        tripRange[3] = 1;

        // Cooldowns
        tripCooldowns[1] = 2;
        tripCooldowns[2] = 30;
        tripCooldowns[3] = 120;

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
        krisDamage[0] = 5;
        krisDamage[1] = 30;
        krisDamage[2] = 10;
        krisDamage[3] = 0;

        // Range
        krisRange[0] = 100;
        krisRange[1] = 10;
        krisRange[2] = 100;
        krisRange[3] = 0;

        // Cooldowns
        krisCooldowns[1] = 10;
        krisCooldowns[2] = 20;
        krisCooldowns[3] = 120;

        krispinnacleDuration = 5;
        /*
        // Stamina costs
        krisbasicSkillStamCost = 5;
        krisadvancedSkillStamCost = 10;
        kristuningSkillStamCost = 0;
        krispinnacleSkillStamCost = 20;
        */
    }
}
