using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripCombat : MonoBehaviour
{
    States state;

    private EnemyHealthScript eHS;
    public GameObject playerContainer;
    private PlayerHealthScript pHS;

    private bool drawGizmos;

    public Transform tripAttackPoint;

    public bool tripIsAlreadyActive;

    [Header("Layers")]
    public LayerMask enemyLayers;

    [Header("Damage Variables")]
    public int[] tripDamage;

    [Header("Attack Range Variables")]
    public int[] tripRange;


    [Header("Cooldown Variables")]
    // Trip
    public float[] tripCooldowns;
    public bool[] tripSkillsReady;
    public float trippinnacleDuration;
    public bool voidPulses;

    /*    
    [Header("Stamina Variables")]
    public float maxStamina
    public float currentStamina;
    public float basicSkillStamCost;
    public float advancedSkillStamCost;
    public float tuningSkillStamCost;
    public float pinnacleSkillStamCost;
    */

    [Header("Animation")]
    public Animator anim;
    private string currentState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        eHS = GetComponent<EnemyHealthScript>();
        pHS = playerContainer.GetComponent<PlayerHealthScript>();

        drawGizmos = false;

        tripSkillsReady[0] = true;
        tripSkillsReady[1] = true;
        tripSkillsReady[2] = true;
        tripSkillsReady[3] = true;

        voidPulses = false;
        SetBaseSkillStats();
    }

    // Update is called once per frame
    void Update()
    {
        CheckActiveChar();
    }

    void CheckActiveChar()
    {
        if (pHS.isTripActive)
        {
            TripAttacks();
        }
        else
        {
            return;
        }
    }

    void TripAttacks()
    {
        // Basic
        if (Input.GetMouseButtonDown(0) && !voidPulses && tripSkillsReady[0])
        {
            StartCoroutine(TripBasic());
        }

        // Void Pulses Basic
        if (Input.GetMouseButtonDown(0) && voidPulses && tripSkillsReady[0])
        {
            StartCoroutine(TripBurstBasic());
        }

        // Advanced
        if (Input.GetKeyDown(KeyCode.Q) && tripSkillsReady[1])
        {
            StartCoroutine(TripAdvanced());
        }

        // Tuning
        if (Input.GetKeyDown(KeyCode.E) && tripSkillsReady[2])
        {
            StartCoroutine(TripTuning());
        }

        // Pinnacle
        if (Input.GetKeyDown(KeyCode.R) && tripSkillsReady[3])
        {
            StartCoroutine(TripBurst());
        }

    }

    void SetBaseSkillStats()
    {
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
        tripCooldowns[0] = 0.5f;
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
    }

    #region Skills
    IEnumerator TripBasic()
    {
        anim.Play("Trip Basic");

        yield return new WaitForSeconds(0.5F);        
        Collider[] hitEnemies = Physics.OverlapSphere(tripAttackPoint.position, tripRange[0], enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            //print("We hit" + enemy.name);
            enemy.GetComponent<EnemyHealthScript>().TakeDamage(tripDamage[0]);
        }

        tripSkillsReady[0] = false;
        Invoke(nameof(ResetCooldownBt), tripCooldowns[0]);

    }

    IEnumerator TripAdvanced()
    {
        anim.Play("Trip Advn");

        yield return new WaitForSeconds(0.3F);

        Collider[] hitEnemies = Physics.OverlapSphere(tripAttackPoint.position, tripRange[1], enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            //print("We hit" + enemy.name);
            enemy.GetComponent<EnemyHealthScript>().TakeDamage(tripDamage[1]);
        }

        tripSkillsReady[1] = false;
        Invoke(nameof(ResetCooldownAt), tripCooldowns[1]);
    }

    IEnumerator TripTuning()
    {
        anim.Play("Trip Tuning");

        Collider[] hitEnemies = Physics.OverlapSphere(tripAttackPoint.position, tripRange[2], enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            //print("We hit" + enemy.name);
            enemy.GetComponent<EnemyHealthScript>().HalveHP();
        }

        tripSkillsReady[2] = false;
        Invoke(nameof(ResetCooldownTt), tripCooldowns[2]);

        yield return null;
    }

    IEnumerator TripBurst()
    {
        voidPulses = true;

        tripSkillsReady[3] = false;
        Invoke(nameof(ResetCooldownPt), tripCooldowns[3]);

        yield return null;
    }

    IEnumerator TripBurstBasic()
    {
        anim.Play("Trip Pinnacle");

        Collider[] hitEnemies = Physics.OverlapSphere(tripAttackPoint.position, tripRange[0], enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            //print("We hit" + enemy.name);
            enemy.GetComponent<EnemyHealthScript>().TakeDamage(tripDamage[3]);
        }

        tripSkillsReady[0] = false;
        Invoke(nameof(ResetCooldownBt), tripCooldowns[0]);

        yield return null;
    }
    #endregion

    // Cooldown resets
    #region Trip
    void ResetCooldownBt()
    {
        tripSkillsReady[0] = true;
        print("Basic skill is ready");
    }
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
    #endregion
}
