using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrisCombat : MonoBehaviour
{
    States state;

    private EnemyHealthScript eHS;
    private PlayerHealthScript pHS;

    private bool drawGizmos;

    public Transform krisAttackPoint;

    public bool krisIsAlreadyActive;

    [Header("Layers")]
    public LayerMask enemyLayers;

    [Header("Damage Variables")]
    public int[] krisDamage;

    [Header("Attack Range Variables")]
    public int[] krisRange;

    [Header("Cooldown Variables")]

    // Kris
    public float[] krisCooldowns;
    public bool[] krisSkillsReady;
    public float krispinnacleDuration;

    /*    
    [Header("Stamina Variables")]
    public float maxStamina
    public float currentStamina;
    public float basicSkillStamCost;
    public float advancedSkillStamCost;
    public float tuningSkillStamCost;
    public float pinnacleSkillStamCost;
    */

    [Header("Projectile")]
    public GameObject krisSpear;
    public float krisSpearSpeed;
    public GameObject crystalSpear;
    public float crystalSpearSpeed;
    public bool krisCanShootTuning;

    [Header("Kris variables")]
    private int maxCrystalSpearCharges = 3;
    private int currentCrystalSpearCharges;
    public bool hasCrystalSpearCharge;

    [Header("Animation")]
    public Animator anim;
    private string currentState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        eHS = GetComponent<EnemyHealthScript>();
        pHS = GetComponent<PlayerHealthScript>();

        drawGizmos = false;

        krisSkillsReady[0] = true;
        krisSkillsReady[1] = true;
        krisSkillsReady[2] = true;
        krisSkillsReady[3] = true;

        maxCrystalSpearCharges = 3;
        krisCanShootTuning = true;
        SetBaseSkillStats();
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
    }

    void CheckState()
    {
        if (pHS.isKrisActive)
        {
            KrisAttacks();
        }
    }

    void KrisAttacks()
    {
        if (Input.GetMouseButtonDown(0) && krisSkillsReady[0])
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

            krisSkillsReady[0] = false;
            Invoke(nameof(ResetCooldownBk), krisCooldowns[0]);
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
                if (krisCanShootTuning)
                {
                    krisCanShootTuning = false;
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

    #region Kris
    void ResetCooldownBk()
    {
        krisSkillsReady[0] = true;
        print("Kris basic skill is ready");
    }

    void ResetCooldownAk()
    {
        krisSkillsReady[1] = true;
        print("Kris advanced skill is ready");
    }

    void ResetCooldownTk()
    {

        krisSkillsReady[2] = true;
        print("Kris tuning skill is ready");
    }

    void ResetCooldownPk()
    {
        krisSkillsReady[3] = true;
        print("Kris pinnacle skill is ready");
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        if (krisAttackPoint == null)
        {
            return;
        }
        else
        {
            drawGizmos = true;
        }
    }

    #region Skills
    IEnumerator KrisBasic()
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

        krisSkillsReady[0] = false;
        Invoke(nameof(ResetCooldownBk), krisCooldowns[0]);

        yield return null;
    }

    IEnumerator KrisAdvanced()
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

        yield return null;
    }

    IEnumerator KrisTuning()
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

            yield return new WaitForSeconds(1F);

        }

        maxCrystalSpearCharges += 1;

        krisSkillsReady[2] = false;
        Invoke(nameof(ResetCooldownTk), krisCooldowns[2]);

        yield return null;
    }

    IEnumerator KrisPinnacle()
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

        yield return null;
    }

    #endregion

    void SetBaseSkillStats()
    {
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
        krisCooldowns[0] = 1.5f;
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

    public void ChangeAnimation(string newState)
    {
        // Stop the animation iterupting itself
        if (currentState == newState) return;

        // Play the animation
        anim.Play(newState);

        // Reassign the currentState
        currentState = newState;
    }
}

