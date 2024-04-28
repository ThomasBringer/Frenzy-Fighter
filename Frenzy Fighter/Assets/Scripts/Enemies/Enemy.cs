using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Class for all enemies.
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    Health health;
    Animator anim;

    [Tooltip("After the enemy is killed, it will get destroyed after this delay.")]
    [SerializeField] float dieDestroyDelay = 5;

    PlayerMove player;

    [SerializeField] Transform graphics;

    NavMeshAgent agent;

    [SerializeField] float updatePlayerTargetDelay = .5f;

    [SerializeField] float playerSightDistance = 8;
    float playerSightDistanceSquared;
    bool chasingPlayer = false;

    [SerializeField] float minWalkPointDistance = 5;
    [SerializeField] float maxWalkPointDistance = 5;
    float RandomWalkPointDistance => Random.Range(minWalkPointDistance, maxWalkPointDistance);

    Vector3 walkPoint;

    void Awake()
    {
        health = GetComponent<Health>();
        anim = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerMove>();
        agent = GetComponentInChildren<NavMeshAgent>();
        playerSightDistanceSquared = playerSightDistance * playerSightDistance;
        FindPatrolPoint();
    }

    void OnEnable()
    {
        EnemiesTracker.Add(this);
        health.onDie.AddListener(Die);
        health.onDamage.AddListener(OnDamage);
    }

    void OnDisable()
    {
        health.onDie.RemoveListener(Die);
        health.onDamage.RemoveListener(OnDamage);
    }

    public bool TryDamage(float damage)
    {
        // If enemy is hit but not chasing player, start chasing him.
        if (!chasingPlayer) StartChasingPlayer();

        return health.Damage(damage);
    }

    public void OnDamage(float damage)
    {
        anim.SetTrigger("damage");
    }

    void Die(float damage)
    {
        anim.SetTrigger("die");
        EnemiesTracker.Remove(this);
        Destroy(gameObject, dieDestroyDelay);
        StopAgent();
    }

    void Update()
    {
        if (!health.dead)
            AliveUpdate();
    }

    void AliveUpdate()
    {
        // Look towards player (but ignore Y-axis)
        graphics.LookAt(transform.position + Vector3.Scale(player.transform.position - transform.position, new Vector3(1, 0, 1)));

        if (!chasingPlayer)
            PatrollingUpdate();
    }

    void PatrollingUpdate()
    {
        CheckWalkPointDistance();
        CheckPlayerSight();
    }

    void CheckWalkPointDistance()
    {
        float distanceToWalkPoint = Vector3.SqrMagnitude(walkPoint - transform.position);
        if (distanceToWalkPoint < .5f)
        {
            FindPatrolPoint();
        }
    }

    void SetDestinationToPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    void SetDestinationToRandom()
    {
        agent.SetDestination(player.transform.position);
    }

    void StopAgent()
    {
        agent.isStopped = true;
        if (chasingPlayer)
            CancelInvoke(nameof(SetDestinationToPlayer));
        else
            CancelInvoke(nameof(SetDestinationToRandom));
    }

    void CheckPlayerSight()
    {
        float distanceSquared = Vector3.SqrMagnitude(player.transform.position - transform.position);
        if (distanceSquared < playerSightDistanceSquared)
        {
            StartChasingPlayer();
        }
    }

    void StartChasingPlayer()
    {
        chasingPlayer = true;
        CancelInvoke(nameof(SetDestinationToRandom));
        InvokeRepeating(nameof(SetDestinationToPlayer), 0, updatePlayerTargetDelay);
    }

    void FindPatrolPoint()
    {
        bool success;
        NavMeshHit hit;
        do
        {
            Vector3 randomDir = Random.onUnitSphere;
            Vector3 offset = RandomWalkPointDistance * randomDir;
            success = NavMesh.SamplePosition(transform.position + offset, out hit, 2, 1);
        } while (!success);

        walkPoint = hit.position;
        agent.SetDestination(walkPoint);
    }
}
