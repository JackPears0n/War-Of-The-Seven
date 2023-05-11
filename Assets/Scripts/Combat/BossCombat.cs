using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

public class BossCombat : MonoBehaviour
{
    public NavMeshAgent agent;

    private Transform player;

    public GameObject playerHealth;
    private PlayerHealthScript pHS;
    private EnemyHealthScript eHS;
    public Transform AttkPnt;

    [Header("Layers")]
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    [Header("Basic Attack")]
    public float basicAttkNum = 0;
    public int basicAttkDmg = 20;
    public float basicAttkRange = 4;
    public float basicAttkCool = 1;
    public bool basicAttk;

    [Header("Advanced Attack")]
    public int advnAttkDmg = 40;
    public float advnAttkRange = 10;
    public float advnAttkCool = 5;
    public bool advnAttk;

    [Header("States")]
    public float fOV;
    public float attackRange;
    public bool playerInFOV;
    public bool playerInAttackRange;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        pHS = playerHealth.GetComponent<PlayerHealthScript>();
        eHS = GetComponent<EnemyHealthScript>();

        basicAttk = true;
        advnAttk = true;
    }

    void Update()
    {
        // Logs the active player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Check sight and attack range
        playerInFOV = Physics.CheckSphere(transform.position, fOV, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInFOV && !playerInAttackRange)
        {
            // play the sleep animation
        }

        if (playerInFOV && !playerInAttackRange)
        {
            // play the awake animation once
            // print("Boss is waking")
            // delay 

            ChasePlayer();
            //print("Boss is chasing");
        }

        if (playerInAttackRange && playerInFOV)
        {
            AttackPlayer();
            //print("Boss is attacking");
        }
    }

    void ChasePlayer()
    {
        // walk anim
        agent.SetDestination(new Vector3(player.position.x, transform.position.y, player.position.z));
        //print(player.position);
    }

    void AttackPlayer()
    {
        // Stops enemy moving
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        // Basic attack
        if (basicAttk && !advnAttk)
        {
            // Attack method here
            CorruptClaws();
            print("Basic attack");
        }

        // advanced attack
        if (advnAttk)
        {
            // Attack method here
            CorruptFlash();
            print("Advn attack");
        }
    }

    // Basic skill
    void CorruptClaws()
    {
        basicAttk = false;

        // play attack anim

        pHS.LoseHealth(basicAttkDmg);
        basicAttkNum++;
        Invoke(nameof(ResetCooldownB), basicAttkCool);
    }

    // Advanced Skill
    void CorruptFlash()
    {
        advnAttk = false;

        // play attack anim

        basicAttkNum = 0;

        pHS.LoseHealth(advnAttkDmg);
        Invoke(nameof(ResetCooldownA), advnAttkCool);
    }

    // Reset cooldowns
    void ResetCooldownB()
    {
        basicAttk = true;
        print("Boss basic skill is ready");
    }

    void ResetCooldownA()
    {
        advnAttk = true;
        print("Boss advn skill is ready");
    }

    // Draw the skill ranges
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttkPnt.position, basicAttkRange);
        Gizmos.DrawWireSphere(AttkPnt.position, advnAttkRange);
        Gizmos.DrawWireSphere(AttkPnt.position, fOV);

    }
}