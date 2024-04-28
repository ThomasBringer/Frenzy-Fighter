using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Class for all enemies.
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [HideInInspector] public Health health;
    Animator anim;

    [Tooltip("After the enemy is killed, it will get destroyed after this delay.")]
    [SerializeField] float dieDestroyDelay = 5;

    Transform player;
    Health playerHealth;

    [SerializeField] Transform graphics;

    NavMeshAgent agent;

    [Tooltip("When chasing player, direction to walk towards will be recalculated every such delay.")]
    [SerializeField] float updatePlayerTargetDelay = .5f;

    [Tooltip("The distance at which the enemy can 'see' (detect and start chasing) the player.")]
    [SerializeField] float playerSightDistance = 8;
    float playerSightDistanceSquared;
    bool chasingPlayer = false;

    [Tooltip("When patrolling, min distance the next walk point will be from the enemy.")]
    [SerializeField] float minWalkPointDistance = 8;
    [Tooltip("When patrolling, max distance the next walk point will be from the enemy.")]
    [SerializeField] float maxWalkPointDistance = 24;
    float RandomWalkPointDistance => Random.Range(minWalkPointDistance, maxWalkPointDistance);

    Vector3 walkPoint;

    [Tooltip("When patrolling, distance at which the next walk point is considered reached.")]
    [SerializeField] float distanceReachWalkPoint = .5f;
    float distanceReachWalkPointSquared;

    [SerializeField] float healthBuffPerEnemyKilled = 25;
    bool damaged = false;

    static int enemyKillCount = 0;

    [Tooltip("Max distance for the enemy to attack the player.")]
    [SerializeField] float attackRange = 2;

    bool attacking = false;

    [Tooltip("Damage the enemy deals to the player.")]
    [SerializeField] float damage = 10;

    WeaponSpawner weaponSpawner;

    [Tooltip("When dropped, weapons will get ejected away from the player by this distance.")]
    [SerializeField] float weaponSpawnEjectDistance = 3;

    Renderer rendrr;
    MaterialPropertyBlock flashWhitePropBlock;
    [SerializeField] Texture flashWhiteTexture;
    [SerializeField] float flashWhiteDuration = .1f;

    void Awake()
    {
        health = GetComponent<Health>();
        anim = GetComponentInChildren<Animator>();

        player = FindObjectOfType<PlayerMove>().transform;
        playerHealth = player.GetComponentInChildren<Health>();

        agent = GetComponentInChildren<NavMeshAgent>();
        playerSightDistanceSquared = playerSightDistance * playerSightDistance;
        distanceReachWalkPointSquared = distanceReachWalkPoint * distanceReachWalkPoint;
        FindPatrolPoint();

        weaponSpawner = FindObjectOfType<WeaponSpawner>();

        rendrr = GetComponentInChildren<Renderer>();
        flashWhitePropBlock = new MaterialPropertyBlock();
        flashWhitePropBlock.SetTexture("_BaseMap", flashWhiteTexture);

        // When enemy spawns, his health is buffed depending on the amount of enemies killed so far.
        health.Heal(enemyKillCount * healthBuffPerEnemyKilled);
    }

    void OnEnable()
    {
        EnemiesTracker.Add(this);
        health.onDie.AddListener(Die);
        health.onDamage.AddListener(OnDamage);
        Health.onDieAny.AddListener(OnAnyEnemyDied);
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

        damaged = true;
        return health.Damage(damage);
    }

    public void OnDamage(float damage)
    {
        FlashIn();
        anim.SetTrigger("damage");
    }

    void Die(float damage)
    {
        anim.SetTrigger("die");
        EnemiesTracker.Remove(this);
        Destroy(gameObject, dieDestroyDelay);
        StopAgent();
        enemyKillCount++;
        DropWeapon();
    }

    void Update()
    {
        if (!health.dead)
            AliveUpdate();
    }

    void AliveUpdate()
    {
        if (chasingPlayer)
            ChasingUpdate();
        else
            PatrollingUpdate();
    }

    float DistanceToPlayerSquared => Vector3.SqrMagnitude(player.transform.position - transform.position);
    bool IsPlayerInAttackRange => DistanceToPlayerSquared < attackRange;

    void ChasingUpdate()
    {
        if (attacking)
        {
            if (!IsPlayerInAttackRange)
                StopAttacking();
        }
        else
        {
            if (IsPlayerInAttackRange)
                StartAttacking();
        }
    }

    void StartAttacking()
    {
        attacking = true;
        anim.SetBool("attacking", true);
    }

    void StopAttacking()
    {
        attacking = false;
        anim.SetBool("attacking", false);
    }

    public void Attack()
    {
        if (IsPlayerInAttackRange)
            playerHealth.Damage(damage);
    }

    void PatrollingUpdate()
    {
        CheckWalkPointDistance();
        CheckPlayerSight();
    }

    void CheckWalkPointDistance()
    {
        float distanceToWalkPointSquared = Vector3.SqrMagnitude(walkPoint - transform.position);
        if (distanceToWalkPointSquared < distanceReachWalkPointSquared)
        {
            FindPatrolPoint();
        }
    }

    void SetDestinationToPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    void StopAgent()
    {
        agent.isStopped = true;
        if (chasingPlayer)
            CancelInvoke(nameof(SetDestinationToPlayer));
    }

    // Check if the player is in sight of the enemy
    void CheckPlayerSight()
    {
        if (DistanceToPlayerSquared < playerSightDistanceSquared)
        {
            StartChasingPlayer();
        }
    }

    void StartChasingPlayer()
    {
        chasingPlayer = true;

        // Find player repeatedly
        InvokeRepeating(nameof(SetDestinationToPlayer), 0, updatePlayerTargetDelay);
    }

    // Find a walk point when patrolling
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

    void OnAnyEnemyDied(float damage, Health h)
    {
        // Check if the enemy who died is the current enemy
        if (h == health)
            return;

        OnOtherEnemyDied();
    }

    void OnOtherEnemyDied()
    {
        // If enemy is undamaged, each enemy killed gives him a health buff.
        if (!damaged)
            health.Heal(healthBuffPerEnemyKilled);
    }

    void DropWeapon()
    {
        Vector3 dir = Vector3.Scale(transform.position - player.position, new Vector3(1, 0, 1)).normalized;
        Vector3 offset = weaponSpawnEjectDistance * dir;
        weaponSpawner.SpawnWeapon(transform.position + offset);
    }

    void FlashIn()
    {
        rendrr.SetPropertyBlock(flashWhitePropBlock);
        Invoke(nameof(FlashOut), flashWhiteDuration);
    }

    void FlashOut()
    {
        rendrr.SetPropertyBlock(null);
    }
}
