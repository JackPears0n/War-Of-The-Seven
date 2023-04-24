using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

public class VoidOrbAttack : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public GameObject playerHealth;
    private PlayerHealthScript pHS;

    [Header("Layers")]
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer; 

    [Header("Patroling")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("Attacking")]
    public float attackCooldown;
    bool alreadyAttacked;
    public int attackDamage;

    [Header("States")]
    public float fOV;
    public float attackRange;
    public bool playerInFOV;
    public bool playerInAttackRange;
 
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        pHS = playerHealth.GetComponent<PlayerHealthScript>();
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
            Patroling();
            //print("patroling");
        }

        if (playerInFOV && !playerInAttackRange)
        {
            ChasePlayer();
            //print("chasing");
        }

        if (playerInAttackRange && playerInFOV)
        {
            AttackPlayer();
        }
    }

    void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // walkPoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        // Calculater random point in range
        float randomz = Random.Range(-walkPointRange, walkPointRange);
        float randomx = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomz);
        //print(walkPoint);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }

    }

    void ChasePlayer()
    {
        agent.SetDestination( new Vector3( player.position.x, transform.position.y, player.position.z) );
        //print(player.position);
    }

    void AttackPlayer()
    {
        // Stops enemy moving
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Attack method here
            VoidFlash();
            print("attack");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void VoidFlash()
    {
        pHS.LoseHealth(attackDamage);
    }

}
