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
    public float advancedSkillCool;
    public float tuningSkillCool;
    public float pinnacleSkillCool;
    public bool advancedSkillReady;
    public bool tuningSkillReady;
    public bool pinnacleSkillReady;

    public float pinnacleDuration;
    public bool voidPulses;

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

        advancedSkillReady = true;
        tuningSkillReady = true;
        pinnacleSkillReady = true;

        voidPulses = false;
        maxCrystalSpearCharges = 3;
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
            advancedSkillCool = 2;
            tuningSkillCool = 30;
            pinnacleSkillCool = 120;

            pinnacleDuration = 30;

            // Stamina costs
            basicSkillStamCost = 5;
            advancedSkillStamCost = 10;
            tuningSkillStamCost = 0;
            pinnacleSkillStamCost = 20;

            TripAttacks();
        }

        if (pHS.isKrisActive)
        {
            // Damage
            basicSkillDmg = 5;
            advancedSkillDmg = 30;
            tuningSkillDmg = 10;
            pinnacleSkillDmg = 0;

            // Range
            basicSkillRange = 100;
            advancedSkillRange = 10;
            tuningSkillRange = 100;
            pinnacleSkillRange = 0;

            // Cooldowns
            advancedSkillCool = 10;
            tuningSkillCool = 20;
            pinnacleSkillCool = 120;

            pinnacleDuration = 30;

            // Stamina costs
            basicSkillStamCost = 5;
            advancedSkillStamCost = 10;
            tuningSkillStamCost = 0;
            pinnacleSkillStamCost = 20;

            KrisAttacks();
        }
    }

    void TripAttacks()
    {
        // Basic
        if (Input.GetMouseButtonDown(0) && !voidPulses)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(tripAttackPoint.position, basicSkillRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(basicSkillDmg);
            }
        }

        // Void Pulses Basic
        if (Input.GetMouseButtonDown(0) && voidPulses)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(tripAttackPoint.position, basicSkillRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(pinnacleSkillDmg);
            }
        }

        // Advanced
        if (Input.GetKeyDown(KeyCode.Q) && advancedSkillReady)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(tripAttackPoint.position, advancedSkillRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(advancedSkillDmg);
            }

            advancedSkillReady = false;
            Invoke(nameof(ResetCooldownA), advancedSkillCool);
        }

        // Tuning
        if (Input.GetKeyDown(KeyCode.E) && tuningSkillReady)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(tripAttackPoint.position, basicSkillRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().HalveHP();
            }

            tuningSkillReady = false;
            Invoke(nameof(ResetCooldownT), tuningSkillCool);
        }

        // Pinnacle
        if (Input.GetKeyDown(KeyCode.R) && pinnacleSkillReady)
        {
            voidPulses = true;

            tuningSkillReady = false;
            Invoke(nameof(ResetCooldownP), pinnacleSkillCool);
        }

    }

    void KrisAttacks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Instanciates the spear gameobject
            GameObject CreateSpear = Instantiate(krisSpear, krisAttackPoint.position, krisAttackPoint.rotation);

            // Array for storing enemies
            Collider[] hitEnemies = Physics.OverlapSphere(krisAttackPoint.position, basicSkillRange, enemyLayers);

            // Makes spear move
            CreateSpear.GetComponent<Rigidbody>().velocity = krisAttackPoint.transform.up * krisSpearSpeed;

            // Damages the hit enemies
            foreach (Collider enemy in hitEnemies)
            {
                print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(basicSkillDmg);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && advancedSkillReady)
        {
            pHS.MakeAShield(50);

            Collider[] hitEnemies = Physics.OverlapSphere(krisAttackPoint.position, advancedSkillRange, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                print("We hit" + enemy.name);
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(advancedSkillDmg);
            }

            advancedSkillReady = false;
            Invoke(nameof(ResetCooldownA), advancedSkillCool);
        }

        if (Input.GetKeyDown(KeyCode.E) && tuningSkillReady)
        {
            
            for (currentCrystalSpearCharges = maxCrystalSpearCharges; currentCrystalSpearCharges > 0; currentCrystalSpearCharges--)
            {
                // Instanciates the spear gameobject
                GameObject CreateSpear = Instantiate(crystalSpear, krisAttackPoint.position, krisAttackPoint.rotation);

                // Array for storing enemies
                Collider[] hitEnemies = Physics.OverlapSphere(krisAttackPoint.position, tuningSkillRange, enemyLayers);

                // Makes spear move
                CreateSpear.GetComponent<Rigidbody>().velocity = krisAttackPoint.transform.up * crystalSpearSpeed;

                // Damages the hit enemies
                foreach (Collider enemy in hitEnemies)
                {
                    print("We hit" + enemy.name);
                    enemy.GetComponent<EnemyHealthScript>().TakeDamage(tuningSkillDmg);
                }

            }
            

            //StartCoroutine("CrystalStorm");

            maxCrystalSpearCharges += 1;

            tuningSkillReady = false;
            Invoke(nameof(ResetCooldownT), tuningSkillCool);
        }

        if (Input.GetKeyDown(KeyCode.R) && pinnacleSkillReady)
        {
            pHS.Invunerability(pinnacleDuration);

            if (pHS.invulnerable)
            {
                Invoke(nameof(pHS.HoT), 25);
            }

            pinnacleSkillReady = false;
            Invoke(nameof(ResetCooldownP), pinnacleSkillCool);
        }
    }

    void ResetCooldownA()
    {
        advancedSkillReady = true;
        print("Advanced skill is ready");
    }

    void ResetCooldownT()
    {

        tuningSkillReady = true;
        print("Tuning skill is ready");
    }

    void ResetCooldownP()
    {
        voidPulses = false;

        pinnacleSkillReady = true;
        print("Pinnacle skill is ready");
    }

    IEnumerable CrystalStorm()
    {
        // Instanciates the spear gameobject
        GameObject CreateSpear = Instantiate(crystalSpear, krisAttackPoint.position, krisAttackPoint.rotation);

        // Array for storing enemies
        Collider[] hitEnemies = Physics.OverlapSphere(krisAttackPoint.position, tuningSkillRange, enemyLayers);

        // Makes spear move
        CreateSpear.GetComponent<Rigidbody>().velocity = krisAttackPoint.transform.up * crystalSpearSpeed;

        // Damages the hit enemies
        foreach (Collider enemy in hitEnemies)
        {
            print("We hit" + enemy.name);
            enemy.GetComponent<EnemyHealthScript>().TakeDamage(tuningSkillDmg);
        }

        yield return new WaitForSeconds(1);
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
            Gizmos.DrawWireSphere(tripAttackPoint.position, basicSkillRange);
            Gizmos.DrawWireSphere(tripAttackPoint.position, advancedSkillRange);
            Gizmos.DrawWireSphere(tripAttackPoint.position, tuningSkillRange);
            Gizmos.DrawWireSphere(tripAttackPoint.position, pinnacleSkillRange);
        }

        if (krisAttackPoint)
        {
            Gizmos.DrawWireSphere(krisAttackPoint.position, basicSkillRange);
            Gizmos.DrawWireSphere(krisAttackPoint.position, advancedSkillRange);
            Gizmos.DrawWireSphere(krisAttackPoint.position, tuningSkillRange);
            Gizmos.DrawWireSphere(krisAttackPoint.position, pinnacleSkillRange);
        }

    }
}
